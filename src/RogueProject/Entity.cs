using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueProject
{
    internal class Entity : Sprite
    {
        public const int HEALTH_DEFAULT = 1;


        private int m_HealthPoint;
        private int m_Damage;
        private int m_Defense;

        public Entity(int _HealthPoint, int m_Damage, int _Defense) { 
            
        }

        
    }
}
