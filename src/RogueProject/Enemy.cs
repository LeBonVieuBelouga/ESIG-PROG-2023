using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using System.Formats.Asn1;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics.Metrics;

namespace RogueProject
{
    /// <summary>
    /// Créer une class permettant au GameCore d'implémenté des ennemis dans le jeu.
    /// </summary>
    internal class Enemy : Entity
    {

        private const uint EXPERIENCE_POINT_DEFAULT = 15;
        private const uint ACTION_POINT_DEFAULT = 1;
        private const float VISION_DEFAULT = 5f;

        private uint m_ExperienceGiven;
        private uint m_ActionPoint;

        protected Vector2 m_Vision;

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
        public Enemy(
                Vector2 _EntityIndex,
                Case[][] _GridOfCase,
                Texture2D _Texture2D,
                uint _HealthPoint = HEALTH_DEFAULT,
                uint _Damage = DAMAGE_DEFAULT,
                uint _Defense = DEFENSE_DEFAULT,
                Vector2 _Vision = new Vector2(),
                Vector2 _Position = new Vector2(),
                uint _ExpericenGiven = EXPERIENCE_POINT_DEFAULT,
                float _Velocity = DEFAULT_ENTITY_VELOCITY,
                Rectangle? _SourceRectangle = null,
                Color _Color = default(Color),
                float _Rotation = DEFAULT_ROTATION,
                Vector2 _Origin = new Vector2(),
                Vector2 _Scale = new Vector2(),
                SpriteEffects _Effect = DEFAULT_EFFECT,
                float _LayerDepth = DEFAULT_LAYER_DEPTH
            ) : base(_EntityIndex, _GridOfCase, _Texture2D, _HealthPoint, _Damage, _Defense, _Position, _Velocity,
                _SourceRectangle, _Color, _Rotation, _Origin, _Scale, _Effect, _LayerDepth)
        {
            if (_Vision == new Vector2()) {
                _Vision = new Vector2(VISION_DEFAULT, VISION_DEFAULT);
            }
            this.SetHealthPoint(_HealthPoint);
            this.SetDamage(_Damage);
            this.SetDefense(_Defense);
            this.SetExperienceGiven(_ExpericenGiven);
            this.SetVision(_Vision);
        }

        /// <summary>
        /// Setter de la vision de l'enemie
        /// </summary>
        /// <param name="_Vision"></param>
        void SetVision(Vector2 _Vision)
        {
            if (_Vision.X % 2 == 1 && _Vision.Y % 2 == 1) {
                this.m_Vision = _Vision;
            } 
        }

        /// <summary>
        /// Getter la vision de l'enemie
        /// </summary>
        /// <param name="_Vision"></param>
        Vector2 GetVision()
        {
            return this.m_Vision;
        }

        /// <summary>
        /// Définit le total d'XP que l'ennemie après sa mort.
        /// </summary>
        /// <param name="_ExpericenGiven"></param>
        void SetExperienceGiven(uint _ExpericenGiven)
        {
            this.m_ExperienceGiven = _ExpericenGiven;
        }

        /// <summary>
        /// Renvoit le total d'XP que l'ennemie donne après sa mort
        /// </summary>
        /// <returns>this.m_ExperienceGiven</returns>
        uint GetExpericenGiven()
        {
            return this.m_ExperienceGiven;
        }

        /// <summary>
        /// Permet à l'Ennemie de mourrir.
        /// </summary>
        public override void Death()
        {
            Debug.WriteLine("Morbius");


        }

        /// <summary>
        /// Permet à l'Ennemie d'attaquer.
        /// </summary>
        public override void Attack(ref Entity _entity)
        {
            uint curr_entityHeathPoint = _entity.GetHealthPoint();

            //Retire une partie des dégats fait par l'enemy sur 
            uint curr_damage = this.m_Damage - _entity.GetDefense();

            curr_entityHeathPoint -= curr_damage;

            _entity.SetHealthPoint(curr_entityHeathPoint);
        }

        /// <summary>
        /// Permet à l'Ennemie de bouger.
        /// </summary>
        public void Move(Case[][] _GridOfCase)
        {
            //Change la couleur du tableau dans sa couleur d'origine
            for (int i = 0; i <= _GridOfCase.Length - 1; i++)
            {
                for (int j = 0; j <= _GridOfCase[i].Length - 1; j++)
                {
                    Color color = new Color(255, 0, 255);
                    _GridOfCase[i][j].SetColor(color);
                }
            }

            bool isPlayer = false;  //<- valeur temporaire

            for (int i = (int)m_Vision.X; i > 0; i--)
            {
                for (int j = (int)m_Vision.Y; j > 0; j--)
                {
                    //if (_GridOfCase[(int)m_Vision.X + i][(int)m_Vision.Y + j].GetLight > 0) {

                    Vector2 centreVision = new Vector2((this.GetIndex().X - m_Vision.X/2 + i), (this.GetIndex().Y- m_Vision.Y/2 +j));

                    // Vérifie si la case est remplis
                    if (_GridOfCase[(int)centreVision.X][(int)centreVision.Y].GetContent() is not null) {
                   
                            Debug.Write("Y a un truc..");
                        
                        //Vérifie si la case contient le joueur
                        if (_GridOfCase[(int)centreVision.X][(int)centreVision.Y].GetContent().GetType().Name == "Player")
                        {
                            //Player trouvé
                            isPlayer = true;
                           
                            MovementDecision(centreVision, _GridOfCase);
                        }
                    }
                }
            }

            if (!isPlayer) {
                Random random = new Random();
                // Générez un nombre aléatoire entre 0 et le nombre total de membres de l'enum.
                int nombreAleatoire = random.Next(Enum.GetValues(typeof(DIRECTION)).Length);

                // Convertissez le nombre aléatoire en une valeur enum.
                DIRECTION directionAleatoire = (DIRECTION)nombreAleatoire;
                OrientationMove(directionAleatoire, _GridOfCase);
            }

            for (int i = (int)m_Vision.X; i > 0; i--)
            {
                for (int j = (int)m_Vision.Y; j > 0; j--)
                {
                    //if (_GridOfCase[(int)m_Vision.X + i][(int)m_Vision.Y + j].GetLight > 0) {

                    Vector2 centreVision = new Vector2((this.GetIndex().X - m_Vision.X / 2 + i), (this.GetIndex().Y - m_Vision.Y / 2 + j));

                    _GridOfCase[(int)centreVision.X][(int)centreVision.Y].SetColor(Color.LightBlue);
                }
            }

        }

        void OrientationMove(DIRECTION _Direction, Case[][] _GridOfCase)
        {
            // Vérifie de quelle côté le joueur veut se déplacer
            switch (_Direction)
            {
                // Gauche
                case DIRECTION.LEFT:

                    // Vérifie que le joueur ne va pas se déplacer en dehors du quadrillage
                    if (this.m_Pos.X - _GridOfCase[0][0].GetTexture().Width >= _GridOfCase[0][0].GetPosition().X - this.GetTexture().Width)
                    {
                        // Change la position du joueur et change son index (son emplacement dans le tableau des cases)
                        this.m_Pos.X -= _GridOfCase[0][0].GetTexture().Width;
                        this.SetIndex(new Vector2(this.m_EntityIndex.X - 1, this.m_EntityIndex.Y), _GridOfCase);
                    }
                    break;
                // Droite
                case DIRECTION.RIGHT:

                    // Vérifie que le joueur ne va pas se déplacer en dehors du quadrillage
                    if (this.m_Pos.X + _GridOfCase[_GridOfCase.Length - 1][_GridOfCase[0].Length - 1].GetTexture().Width <= _GridOfCase[_GridOfCase.Length - 1][_GridOfCase[0].Length - 1].GetPosition().X)
                    {
                        // Change la position du joueur et change son index (son emplacement dans le tableau des cases)
                        this.m_Pos.X += _GridOfCase[_GridOfCase.Length - 1][_GridOfCase[0].Length - 1].GetTexture().Width;
                        this.SetIndex(new Vector2(this.m_EntityIndex.X + 1, this.m_EntityIndex.Y), _GridOfCase);
                    }
                    break;
                // Haut
                case DIRECTION.UP:
                    // Vérifie que le joueur ne va pas se déplacer en dehors du quadrillage
                    if (this.m_Pos.Y - _GridOfCase[0][0].GetTexture().Width >= _GridOfCase[0][0].GetPosition().Y - this.GetTexture().Height)
                    {
                        // Change la position du joueur et change son index (son emplacement dans le tableau des cases)
                        this.m_Pos.Y -= _GridOfCase[0][0].GetTexture().Width;
                        this.SetIndex(new Vector2(this.m_EntityIndex.X, this.m_EntityIndex.Y - 1), _GridOfCase);
                    }
                    break;
                // Bas
                case DIRECTION.DOWN:

                    // Vérifie que le joueur ne va pas se déplacer en dehors du quadrillage
                    if (this.m_Pos.Y + _GridOfCase[_GridOfCase.Length - 1][_GridOfCase[0].Length - 1].GetTexture().Width <= _GridOfCase[_GridOfCase.Length - 1][_GridOfCase[0].Length - 1].GetPosition().Y)
                    {
                        // Change la position du joueur et change son index (son emplacement dans le tableau des cases)
                        this.m_Pos.Y += _GridOfCase[_GridOfCase.Length - 1][_GridOfCase[0].Length - 1].GetTexture().Width;
                        this.SetIndex(new Vector2(this.m_EntityIndex.X, this.m_EntityIndex.Y + 1), _GridOfCase);
                    }
                    break;
                case DIRECTION.NONE:
                    // Vérifie que le joueur ne va pas se déplacer en dehors du quadrillage
                    Debug.Write("Rompiche");
                    break;
            }
        }

        void MovementDecision(Vector2 _PlayerIndex, Case[][] _GridOfCase)
        {

            Entity Player = (Player)_GridOfCase[(int)_PlayerIndex.X][(int)_PlayerIndex.Y].GetContent();
            Vector2 curr_PlayerIndex = Player.GetIndex();//Récupère la position dans l'index du joueur
                                                         //Vector2 curr_PlayerIndex = new Vector2(m_Vision.X + i, m_Vision.Y + j);

            //Colorie la case du joueur pour la mettre en surbriance
            _GridOfCase[(int)curr_PlayerIndex.X][(int)curr_PlayerIndex.Y].SetColor(Color.White);

            Vector2 newEntityIndex = new Vector2();

            DIRECTION EnemyDirection = DIRECTION.NONE;

            switch (this.GetIndex())
            {
                case Vector2 a when a == m_EntityIndex:
                    Debug.Write("BACKROOM");
                    break;
                // XXXXXXXXXXXXXXXXX //
                case Vector2 a when a.X > m_EntityIndex.X:
                    newEntityIndex.X++;
                    EnemyDirection = DIRECTION.RIGHT;
                    break;
                case Vector2 a when a.X < m_EntityIndex.X:
                    newEntityIndex.X--;
                    EnemyDirection = DIRECTION.LEFT;
                    break;
                case Vector2 a when a.X++ == m_EntityIndex.X && a.Y == m_EntityIndex.Y || a.X-- == m_EntityIndex.X && a.Y == m_EntityIndex.Y:
                    this.Attack(ref Player);
                    break;
                // YYYYYYYYYYYYYYYYY //
                case Vector2 a when a.Y > m_EntityIndex.Y:
                    newEntityIndex.Y++;
                    EnemyDirection = DIRECTION.DOWN;
                    break;
                case Vector2 a when a.Y < m_EntityIndex.Y:
                    newEntityIndex.Y--;
                    EnemyDirection = DIRECTION.UP;
                    break;
                case Vector2 a when a.Y++ == m_EntityIndex.Y && a.X == m_EntityIndex.X || a.Y-- == m_EntityIndex.Y && a.X == m_EntityIndex.X:
                    this.Attack(ref Player);
                    break;
            }

            OrientationMove(EnemyDirection, _GridOfCase);

            //return _EnemyAxe;
        }


        public void Update(GameTime _GameTime,
                Case[][] _GridOfCase
                )
        {
            this.Move(_GridOfCase);

        }
    }
}

