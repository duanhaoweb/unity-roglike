using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{

    [SerializeField] private Animator monsteranim;
    [SerializeField] private SpriteRenderer monsterSprite;
    private Rigidbody monsterrb;
    private Vector3 monstermovement;

    private const string IS_MONSTER_WALK_PARAM = "IsMonsterWalk";
    // Start is called before the first frame update

    private void Start()
    {
        monsterrb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame


}
