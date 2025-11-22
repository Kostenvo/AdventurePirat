using System;
using UnityEngine;

namespace Creatures.Hero
{
    public class BlockInput : MonoBehaviour
    {
        private InputHero _input;


        private void Start()
        {
            _input ??= FindAnyObjectByType<InputHero>();
        }


        public void ActiveInput(bool block)
        {
            _input.enabled = block;
        }
    }
}