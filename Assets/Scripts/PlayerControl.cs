using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour {

    public GameObject GameManagerGO;

    public GameObject bulletPosition1;
    public GameObject bulletPosition2;
    public GameObject playerBulletGo;
    public GameObject ExplosionGO;

    public Text LivesUIText;

    const int MaxLives = 3;
    int lives;

    public float speed;

    public void Init()
    {
        lives = MaxLives;

        LivesUIText.text = lives.ToString();

        transform.position = new Vector2(0, 0);

        gameObject.SetActive(true);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //fire bullets when spacebar pressed
        if(Input.GetKeyDown("space"))
        {
            GetComponent<AudioSource>().Play();

            GameObject bullet1 = (GameObject)Instantiate(playerBulletGo);
            bullet1.transform.position = bulletPosition1.transform.position;

            GameObject bullet2 = (GameObject)Instantiate(playerBulletGo);
            bullet2.transform.position = bulletPosition2.transform.position;
        }

        //get x/y input
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 direction = new Vector2(x, y).normalized;

        Move(direction);
	}

    void Move(Vector2 direction)
    {
        //get screen limits of player movement
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)); //bottom-left of screen
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)); //top-right of screen

        //account for player sprite width
        max.x = max.x - 0.225f;
        min.x = min.x + 0.225f;

        max.y = max.y - 0.285f;
        min.y = min.y + 0.285f;

        //get player current position
        Vector2 pos = transform.position;

        //calculate new position
        pos += direction * speed * Time.deltaTime;

        //clamp position to inside screen
        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        //update position
        transform.position = pos;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "EnemyShipTag" || collision.tag == "EnemyBulletTag")
        {
            PlayExplosion();
            lives--;
            LivesUIText.text = lives.ToString();
            if(lives == 0)
            {
                GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.GAMEOVER);
                gameObject.SetActive(false);
            }
        }
    }

    void PlayExplosion()
    {
        GameObject explosion = Instantiate(ExplosionGO);
        explosion.transform.position = transform.position;
    }
}
