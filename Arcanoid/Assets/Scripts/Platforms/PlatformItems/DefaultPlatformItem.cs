using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SekiburaGames.Arkanoid.Gameplay
{
    public class DefaultPlatformItem : BasePlatformItem
    {
        private void Start()
        {
            //StartCoroutine(dsdfsd()); 
        }

        IEnumerator dsdfsd()
        {
            yield return new WaitForSecondsRealtime(10f);
            this.ApplyDamage(1);
        }
    }
}
