using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using System.Formats.Asn1;

namespace RogueProject
{
    /// <summary>
    /// Classe représentant une Entity (entité) qui peut s'apparenter à un être vivant
    /// Cette classe est abstraite donc impossible de créer un objet de ce type
    /// Cette classe hérite de la classe Sprite
    /// </summary>
    internal abstract class Entity : Sprite
    {
        protected const int HEALTH_DEFAULT = 1;
        protected const int DAMAGE_DEFAULT = 0;
        protected const int DEFENSE_DEFAULT = 0;
        protected const float DEFAULT_ENTITY_VELOCITY = 1f;

        protected Vector2 m_EntityIndex = new Vector2(0, 0);
        protected int m_HealthPoint;
        protected int m_Damage;
        protected int m_Defense;
        protected bool m_IsDead = false;

        /// <summary>
        /// Créer un objet de type Entity et instencie toutes ces propriétés.
        /// Permet de généré des créatures,joueur etc.. pouvant bouger, attaquer, mourrir, etc..
        /// </summary>
        /// <param name="_Texture2D">Texture de le Ground</param>
        /// <param name="_Position">Position X et Y (Vecteur 2d) du Ground</param>
        /// <param name="_Velocity">vitesse du Ground</param>
        /// <param name="_SourceRectangle">Taille du Ground</param>
        /// <param name="_Color">Couleur du Ground</param>
        /// <param name="_Rotation">Orientation du Ground</param>
        /// <param name="_Origin">Position d'origin du Ground</param>
        /// <param name="_Scale">Mise à l'échelle du Ground</param>
        /// <param name="_Effect">Modificateurs pour le dessin (peut être combiné)</param>
        /// <param name="_LayerDepth">Profondeur du champ du Ground/param>
        public Entity(
            Vector2 _EntityIndex,
            Case[][] _GridOfCase,
            Texture2D _Texture2D,
            int _HealthPoint = HEALTH_DEFAULT,
            int _Damage = DAMAGE_DEFAULT,
            int _Defense = DEFENSE_DEFAULT,
            Vector2 _Position = new Vector2(),
            float _Velocity = DEFAULT_ENTITY_VELOCITY,
            Rectangle? _SourceRectangle = null,
            Color _Color = default(Color),
            float _Rotation = DEFAULT_ROTATION,
            Vector2 _Origin = new Vector2(),
            Vector2 _Scale = new Vector2(),
            SpriteEffects _Effect = DEFAULT_EFFECT,
            float _LayerDepth = DEFAULT_LAYER_DEPTH
            ) : base (_Texture2D, _Position, _Velocity, _SourceRectangle, _Color, _Rotation, _Origin, _Scale, _Effect, _LayerDepth) {


            this.SetIndex(_EntityIndex, _GridOfCase);
            this.SetHealthPoint(_HealthPoint);
            this.SetDamage(_Damage);
            this.SetDefense(_Defense);

        }

        /// <summary>
        /// Change l'index (emplacement dans le tableau des cases) du joueur
        /// </summary>
        /// <param name="_CurrIndex">Nouvelle index dans le tableau des cases</param>
        /// <param name="_GridOfCase">Le tableau de cases</param>
        public void SetIndex(Vector2 _CurrIndex, Case[][] _GridOfCase)
        {
            // Vérifie si le nouvel index est valide sinon empêche sa modification
            if (_CurrIndex.X < 0 || _CurrIndex.Y < 0)
            {
                Debug.WriteLine("WELCOME TO THE BACKROOM");
                //GameCore.SetBackRoomMode();
                return;
            }

            // Affiche son ancienne index
            //Debug.WriteLine("Content supprimé à : " + m_EntityIndex.X + ";" + m_EntityIndex.Y);

            // Retire la préscence du joueur dans sa case précédente
            _GridOfCase[(int)m_EntityIndex.X][(int)m_EntityIndex.Y].SetContent(null);

            this.m_EntityIndex = _CurrIndex;

            // Affiche son nouvelle index
            //Debug.WriteLine("Content ajouté à : " + _CurrIndex.X + ";" + _CurrIndex.Y);

            // Ajoute la préscence du joueur dans la nouvelle case
            _GridOfCase[(int)m_EntityIndex.X][(int)m_EntityIndex.Y].SetContent(this);

        }

        /// <summary>
        /// Permet de déplacer le l'entité dans une gride donné
        /// </summary>
        /// <param name="_Direction"></param>
        /// <param name="_GridOfCase"></param>
        protected void OrientationMove(DIRECTION _Direction, Case[][] _GridOfCase)
        {
            // Vérifie de quelle côté le joueur veut se déplacer
            switch (_Direction)
            {
                // Gauche
                case DIRECTION.LEFT:

 

                    // Vérifie que le joueur ne va pas se déplacer en dehors du quadrillage
                    if (this.m_Pos.X - _GridOfCase[0][0].GetTexture().Width >= _GridOfCase[0][0].GetPosition().X - this.GetTexture().Width)
                    {

 

                        // Vérifie si la case dans laquelle le joueur veut se déplacer est vide et qu'on peut marcher dessus
                        if (_GridOfCase[(int)this.m_EntityIndex.X - 1][(int)this.m_EntityIndex.Y].GetIsWalkable() && _GridOfCase[(int)this.m_EntityIndex.X - 1][(int)this.m_EntityIndex.Y].GetContent() is null)
                        {                        
                            // Change la position du joueur et change son index (son emplacement dans le tableau des cases)
                            this.m_Pos.X -= _GridOfCase[0][0].GetTexture().Width;
                            this.SetIndex(new Vector2(this.m_EntityIndex.X - 1, this.m_EntityIndex.Y), _GridOfCase);
                        }
                    }
                    break;
                // Droite
                case DIRECTION.RIGHT:

 

                    // Vérifie que le joueur ne va pas se déplacer en dehors du quadrillage
                    if (this.m_Pos.X + _GridOfCase[_GridOfCase.Length - 1][_GridOfCase[0].Length - 1].GetTexture().Width <= _GridOfCase[_GridOfCase.Length - 1][_GridOfCase[0].Length - 1].GetPosition().X)
                    {

 

                        // Vérifie si la case dans laquelle le joueur veut se déplacer est vide et qu'on peut marcher dessus
                        if (_GridOfCase[(int)this.m_EntityIndex.X + 1][(int)this.m_EntityIndex.Y].GetIsWalkable() && _GridOfCase[(int)this.m_EntityIndex.X + 1][(int)this.m_EntityIndex.Y].GetContent() is null)
                        {
                            // Change la position du joueur et change son index (son emplacement dans le tableau des cases)
                            this.m_Pos.X += _GridOfCase[_GridOfCase.Length - 1][_GridOfCase[0].Length - 1].GetTexture().Width;
                            this.SetIndex(new Vector2(this.m_EntityIndex.X + 1, this.m_EntityIndex.Y), _GridOfCase);
                        }
                    }
                    break;
                // Haut
                case DIRECTION.UP:

 

                    // Vérifie que le joueur ne va pas se déplacer en dehors du quadrillage
                    if (this.m_Pos.Y - _GridOfCase[0][0].GetTexture().Width >= _GridOfCase[0][0].GetPosition().Y - this.GetTexture().Height)
                    {

 

                        // Vérifie si la case dans laquelle le joueur veut se déplacer est vide et qu'on peut marcher dessus
                        if (_GridOfCase[(int)this.m_EntityIndex.X][(int)this.m_EntityIndex.Y - 1].GetIsWalkable() && _GridOfCase[(int)this.m_EntityIndex.X][(int)this.m_EntityIndex.Y - 1].GetContent() is null)
                        {
                            // Change la position du joueur et change son index (son emplacement dans le tableau des cases)
                            this.m_Pos.Y -= _GridOfCase[0][0].GetTexture().Width;
                            this.SetIndex(new Vector2(this.m_EntityIndex.X, this.m_EntityIndex.Y - 1), _GridOfCase);
                        }
                    }
                    break;
                // Bas
                case DIRECTION.DOWN:

 

                    // Vérifie que le joueur ne va pas se déplacer en dehors du quadrillage
                    if (this.m_Pos.Y + _GridOfCase[_GridOfCase.Length - 1][_GridOfCase[0].Length - 1].GetTexture().Width <= _GridOfCase[_GridOfCase.Length - 1][_GridOfCase[0].Length - 1].GetPosition().Y)
                    {

 

                        // Vérifie si la case dans laquelle le joueur veut se déplacer est vide et qu'on peut marcher dessus
                        if (_GridOfCase[(int)this.m_EntityIndex.X][(int)this.m_EntityIndex.Y + 1].GetIsWalkable() && _GridOfCase[(int)this.m_EntityIndex.X][(int)this.m_EntityIndex.Y + 1].GetContent() is null)
                        {
                            // Change la position du joueur et change son index (son emplacement dans le tableau des cases)
                            this.m_Pos.Y += _GridOfCase[_GridOfCase.Length - 1][_GridOfCase[0].Length - 1].GetTexture().Width;
                            this.SetIndex(new Vector2(this.m_EntityIndex.X, this.m_EntityIndex.Y + 1), _GridOfCase);
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// Setter pour m_HealthPoint
        /// </summary>
        /// <param name="_HealthPoint"></param>
        public void SetHealthPoint(int _HealthPoint = HEALTH_DEFAULT) {
            if (_HealthPoint == 0f)
            {
                Debug.WriteLine("Les points de vie sont passé à zero");
                this.m_IsDead = true;
                this.Death();
            }
            else {
                this.m_HealthPoint = _HealthPoint;
            }
        }
        /// <summary>
        /// Getter pour m_HealthPoint
        /// </summary>
        /// <returns>this.m_HealthPoint</returns>
        public int  GetHealthPoint()
        {
                return this.m_HealthPoint;
        }

        /// <summary>
        /// Setter pour m_Damage 
        /// </summary>
        /// <param name="_Damage"></param>
        public void SetDamage(int _Damage = DAMAGE_DEFAULT) {
          
                this.m_Damage = _Damage;
        }

        /// <summary>
        /// Getter pour m_Damage
        /// </summary>
        /// <returns></returns>
        public int GetDamage()
        {
            return this.m_Damage;
        }

        /// <summary>
        /// Setter pour m_Defense
        /// </summary>
        /// <param name="_Defense"></param>
        public void SetDefense(int _Defense = DEFENSE_DEFAULT) { 
        
            this.m_Defense = _Defense;

        }

        /// <summary>
        /// Getter pour m_Defense
        /// </summary>
        /// <returns></returns>
        public int GetDefense()
        {
            return this.m_Defense;

        }

        /// <summary>
        /// Getter pour m_Defense
        /// </summary>
        /// <returns></returns>
        public Vector2 GetIndex()
        {
            return this.m_EntityIndex;

        }

        /// <summary>
        /// Permet de tuer l'Entity.
        /// </summary>
        public abstract void Death();


        /// <summary>
        /// Permet à l'Entity d'attaquer.
        /// </summary>
        public abstract void Attack(ref Entity _entity);
    }
}
