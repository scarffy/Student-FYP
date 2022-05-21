using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FYP.Common
{
    public class MonsterManager : Singleton<MonsterManager>
    {
        [SerializeField] GameObject GolemPrefab;
        [SerializeField] GameObject DesertGolemPrefab;
        [SerializeField] GameObject AncientWarriorPrefab;
        [SerializeField] GameObject SpiritDemonPrefab;
        [SerializeField] GameObject TrollPrefab;
        [SerializeField] GameObject ForestGuardianPrefab;


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}