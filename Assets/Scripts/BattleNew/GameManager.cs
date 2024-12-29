using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<SingleTile> tiles;
    public List<PlayerInBattle> players;
    private PlayerInBattle currentPlayer;
    private float speed = 5f;
    private int playerTurn;
    private bool isMoving = false;
    private bool isAttacking = false;

    private List<SingleTile> enermyCanMoveTileList;

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        EventHandler.OnMovePlayer += OnMovePlayer;
        EventHandler.OnGetAttackDirection += ShowAttackRange;
        // EventHandler.OnPassPlayerToBattleScene += PassPlayerToBattleScene;
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {
        EventHandler.OnMovePlayer -= OnMovePlayer;
        EventHandler.OnGetAttackDirection -= ShowAttackRange;
        // EventHandler.OnPassPlayerToBattleScene -= PassPlayerToBattleScene;
    }

    // private void PassPlayerToBattleScene(List<PlayerInBattle> list)
    // {
    //     Debug.Log("PassPlayerToBattleScene");
    //     players = list;
    // }

    private void OnMovePlayer(Transform transform)
    {
        if (currentPlayer != null)
        {
            if (transform.GetComponent<SingleTile>().canWalk && transform.GetComponent<SingleTile>().inRange)
            {
                MovePlayer(transform);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // foreach(var player in players)
        // {
        //     // 5以内的随机整数位置
        //     player.transform.position = new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), 0);
        // }
        playerTurn = 0;
        currentPlayer = players[playerTurn];
        ShowCanMoveTile(currentPlayer);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTurn >= players.Count)
        {
            playerTurn = 0;
        }
        if (!isMoving)
        {
            currentPlayer = players[playerTurn];
            if (currentPlayer.tag == "Player")
            {
                ShowCanMoveTile(currentPlayer);
            }
            else
            {
                // 敌人移动
                EnermyMove();
            }
        }
        if (isAttacking)
        {
            ShowAttackRange();
        }
    }

    void MovePlayer(Transform tilePosition)
    {
        StartCoroutine(CoMovePlayer(tilePosition));
    }

    IEnumerator CoMovePlayer(Transform tilePosition)
    {
        isMoving = true;
        // 先进行横向移动
        while (currentPlayer.transform.position.x != tilePosition.position.x)
        {
            // 根据目标位置的x坐标，判断角色的朝向
            if (currentPlayer.transform.position.x > tilePosition.position.x)
            {
                currentPlayer.transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else
            {
                currentPlayer.transform.eulerAngles = new Vector3(0, 180, 0);
            }
            currentPlayer.transform.position = Vector3.MoveTowards(currentPlayer.transform.position, new Vector3(tilePosition.position.x, currentPlayer.transform.position.y, 0), speed * Time.deltaTime);
            yield return null;
        }
        // 再进行纵向移动
        while (currentPlayer.transform.position.y != tilePosition.position.y)
        {
            currentPlayer.transform.position = Vector3.MoveTowards(currentPlayer.transform.position, new Vector3(currentPlayer.transform.position.x, tilePosition.position.y, 0), speed * Time.deltaTime);
            yield return null;
        }
        // 移动结束，isMoving设置为false
        isMoving = false;
        // 移动结束，再次到当前角色回合
        playerTurn++;
        // 移动结束后，重置所有tile的颜色
        ResetTilesColor();
    }

    void ResetTilesColor()
    {
        foreach (var tile in tiles)
        {
            Color color = Color.white;
            color.a = 100f / 255f;
            tile.GetComponent<SpriteRenderer>().color = color;
            tile.inRange = false;
        }
    }


    void ShowCanMoveTile(PlayerInBattle player)
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            float disX = Mathf.Abs(tiles[i].transform.position.x - player.transform.position.x);
            float disY = Mathf.Abs(tiles[i].transform.position.y - player.transform.position.y);
            // 如果x+y在3格以内
            if (disX + disY <= player.moveRange && tiles[i].canWalk)
            {
                tiles[i].GetComponent<SpriteRenderer>().color = Color.green;
                tiles[i].inRange = true;
            }
        }
    }

    private Vector3 GetAttackDirection()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 playerPosition = players[0].transform.position;
        Vector3 direction = mousePosition - playerPosition;
        Vector3 dir = Vector3.zero;
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                dir = Vector3.right;
            }
            else
            {
                dir = Vector3.left;
            }
        }
        else
        {
            if (direction.y > 0)
            {
                dir = Vector3.up;
            }
            else
            {
                dir = Vector3.down;
            }
        }
        return dir;
    }

    void ShowAttackRange()
    {
        isAttacking = true;
        Vector3 dir = GetAttackDirection();
        // 先清除所有颜色
        ResetTilesColor();
        // 以player为中心，如果attackDirection为up，那么向上3格变为红色，以此类推
        for (int i = 0; i < tiles.Count; i++)
        {
            float disX = Mathf.Abs(tiles[i].transform.position.x - currentPlayer.transform.position.x);
            float disY = Mathf.Abs(tiles[i].transform.position.y - currentPlayer.transform.position.y);
        
            if (dir == Vector3.up)
            {
                if (tiles[i].transform.position.x == currentPlayer.transform.position.x && Mathf.Abs(tiles[i].transform.position.y - currentPlayer.transform.position.y) <= 3 && tiles[i].transform.position.y > currentPlayer.transform.position.y)
                {
                    tiles[i].GetComponent<SpriteRenderer>().color = Color.red;
                }
            }
            else if (dir == Vector3.down)
            {
                if (tiles[i].transform.position.x == currentPlayer.transform.position.x && Mathf.Abs(tiles[i].transform.position.y - currentPlayer.transform.position.y) <= 3 && tiles[i].transform.position.y < currentPlayer.transform.position.y)
                {
                    tiles[i].GetComponent<SpriteRenderer>().color = Color.red;
                }
            }
            else if (dir == Vector3.left)
            {
                if (tiles[i].transform.position.y == currentPlayer.transform.position.y && Mathf.Abs(tiles[i].transform.position.x - currentPlayer.transform.position.x) <= 3 && tiles[i].transform.position.x < currentPlayer.transform.position.x)
                {
                    tiles[i].GetComponent<SpriteRenderer>().color = Color.red;
                }
            }
            else if (dir == Vector3.right)
            {
                if (tiles[i].transform.position.y == currentPlayer.transform.position.y && Mathf.Abs(tiles[i].transform.position.x - currentPlayer.transform.position.x) <= 3 && tiles[i].transform.position.x > currentPlayer.transform.position.x)
                {
                    tiles[i].GetComponent<SpriteRenderer>().color = Color.red;
                }
            }
        }
    }

    void EnermyMove()
    {
        // 敌人移动
        // 先获取到玩家的位置
        Vector3 playerPosition = players[0].transform.position;
        // 获取到敌人的位置
        Vector3 enermyPosition = players[1].transform.position;
        // 先计算敌人可以移动的位置
        enermyCanMoveTileList = new List<SingleTile>();
        for (int i = 0; i < tiles.Count; i++)
        {
            float disX = Mathf.Abs(tiles[i].transform.position.x - enermyPosition.x);
            float disY = Mathf.Abs(tiles[i].transform.position.y - enermyPosition.y);
            // 如果x+y在3格以内
            if (disX + disY <= 3 && tiles[i].canWalk)
            {
                enermyCanMoveTileList.Add(tiles[i]);
            }
        }
        // 计算enermy可以移动的位置中，离玩家最近的位置
        SingleTile nearestTile = enermyCanMoveTileList[0];
        for (int i = 1; i < enermyCanMoveTileList.Count; i++)
        {
            if (Mathf.Abs(enermyCanMoveTileList[i].transform.position.x - playerPosition.x) + Mathf.Abs(enermyCanMoveTileList[i].transform.position.y - playerPosition.y) < Mathf.Abs(nearestTile.transform.position.x - playerPosition.x) + Mathf.Abs(nearestTile.transform.position.y - playerPosition.y))
            {
                nearestTile = enermyCanMoveTileList[i];
            }
        }
        // 移动到最近的位置
        StartCoroutine(CoMovePlayer(nearestTile.transform));

    }
}
