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
using System.Reflection;

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
                int _HealthPoint = HEALTH_DEFAULT,
                int _Damage = DAMAGE_DEFAULT,
                int _Defense = DEFENSE_DEFAULT,
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
            if (_Vision.X % 2 == 1 && _Vision.Y % 2 == 1)
            {
                this.m_Vision = _Vision;
            }
            else {
                this.m_Vision = new Vector2(VISION_DEFAULT, VISION_DEFAULT);
                Debug.WriteLine("la vision de l'ennemie n'est pas impaire, la valeur par defaut a ete attribue");
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
            int curr_entityHeathPoint = _entity.GetHealthPoint();

            //Retire une partie des dégats fait par l'enemy sur 
            int curr_damage = this.m_Damage - _entity.GetDefense();

            curr_entityHeathPoint -= curr_damage;

            _entity.SetHealthPoint(curr_entityHeathPoint);

            Debug.WriteLine(_entity.GetHealthPoint());
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

            //Définit la direction que va prendre l'enemie
            DIRECTION DirectionEnemy = DIRECTION.NONE;

            bool isPlayer = false;

            Vector2 IndexPlayer = new Vector2();

            for (int i = (int)m_Vision.X; i > 0; i--)
            {
                for (int j = (int)m_Vision.Y; j > 0; j--)
                {
                    //if (_GridOfCase[(int)m_Vision.X + i][(int)m_Vision.Y + j].GetLight > 0) {

                    //Définit la position de la case à traité
                    Vector2 curr_IndexCase = new Vector2((this.GetIndex().X - m_Vision.X / 2 + i), (this.GetIndex().Y - m_Vision.Y / 2 + j));

                    //Vérifie que la case existe
                    if (curr_IndexCase.X >= 0 && curr_IndexCase.Y >= 0 && (int)curr_IndexCase.X <= _GridOfCase.Length - 1 && curr_IndexCase.Y <= _GridOfCase[0].Length - 1)
                    {
                        // Vérifie si la case est remplis
                        if (_GridOfCase[(int)curr_IndexCase.X][(int)curr_IndexCase.Y].GetContent() is not null)
                        {
                            //Vérifie si la case contient le joueur
                            if (_GridOfCase[(int)curr_IndexCase.X][(int)curr_IndexCase.Y].GetContent().GetType().Name == "Player")
                            {
                                //Player trouvé
                                isPlayer = true;

                                
                                IndexPlayer = curr_IndexCase;

                                // Définit la direction que va prendre l'ennemie
                                DirectionEnemy = MovementDecision(curr_IndexCase, _GridOfCase);
                            }
                        }
                    }
                }
            }
            
            if (!isPlayer)
            {
                Random random = new Random();
                // Générez un nombre aléatoire entre 0 et le nombre total de membres de l'enum.
                int nombreAleatoire = random.Next(Enum.GetValues(typeof(DIRECTION)).Length);

                // Convertissez le nombre aléatoire en une valeur enum.
                DirectionEnemy = (DIRECTION)nombreAleatoire;
            }

            OrientationMove(DirectionEnemy, _GridOfCase);

            //Permet de visualiser la vision de l'enemie
            for (int i = (int)m_Vision.X; i > 0; i--)
            {
                for (int j = (int)m_Vision.Y; j > 0; j--)
                {
                    //Définit la position de la case à traité
                    Vector2 curr_IndexCase = new Vector2(this.GetIndex().X - m_Vision.X / 2 + i,this.GetIndex().Y - m_Vision.Y / 2 + j) ;

                    // Vérifie que l'index a colorier est valide
                    if (curr_IndexCase.X >= 0 && curr_IndexCase.Y >= 0 && (int)curr_IndexCase.X <= _GridOfCase.Length-1 &&curr_IndexCase.Y <= _GridOfCase[0].Length -1) {
                        _GridOfCase[(int)curr_IndexCase.X][(int)curr_IndexCase.Y].SetColor(Color.LightBlue);
                    }
                    
                }
            }

            /*
            if (IndexPlayer == new Vector2()) {
                //Colorie la case du joueur pour la mettre en surbriance
                _GridOfCase[(int)IndexPlayer.X][(int)IndexPlayer.Y].SetColor(Color.GreenYellow);
            }
            */
            
        }


        DIRECTION MovementDecision(Vector2 _PlayerIndex, Case[][] _GridOfCase)
        {
            Entity Player = (Player)_GridOfCase[(int)_PlayerIndex.X][(int)_PlayerIndex.Y].GetContent();

            _PlayerIndex.X -= 0.5f;
            _PlayerIndex.Y -= 0.5f;

            DIRECTION EnemyDirection = DIRECTION.NONE;

            if (m_EntityIndex.X == _PlayerIndex.X && m_EntityIndex.Y == _PlayerIndex.Y)
            {
                //Le joueur est sur la meme position que l'ennemie
                Debug.WriteLine("BACKROOM");

            }
            else
            {
                Random random = new Random();
                int randMaxChose = random.Next(2);

                switch (m_EntityIndex)
                {
                    case Vector2 EIndex when EIndex.X > _PlayerIndex.X:
                        if (EIndex.Y == _PlayerIndex.Y)
                        {
                            // L'ennemi est à droite du joueur
                            EnemyDirection = DIRECTION.LEFT;
                            Debug.WriteLine("Ennemi va à GAUCHE");
                        }
                        else if (EIndex.Y < _PlayerIndex.Y)
                        {
                            // Le joueur est en bas à gauche

                            // Générez un nombre aléatoire entre 1 et 3 soit la valeur UP ou RIGHT
                            int RandLEFTDOWN = randMaxChose * 2 + 1;
                            // Convertissez le nombre aléatoire en une valeur enum.
                            EnemyDirection = (DIRECTION)RandLEFTDOWN;
                            Debug.WriteLine("Ennemi va à GAUCHE/BAS");
                        }
                        else if (EIndex.Y > _PlayerIndex.Y)
                        {
                            // Le joueur est en haut à gauche

                            // Générez un nombre aléatoire entre 0 et 2 soit la valeur UP ou RIGHT
                            int RandLEFTUP = randMaxChose * 3;
                            // Convertissez le nombre aléatoire en une valeur enum.
                            EnemyDirection = (DIRECTION)RandLEFTUP;
                            Debug.WriteLine("Ennemi va à GAUCHE/HAUT");
                        }
                        break;
                    case Vector2 EIndex when EIndex.X < _PlayerIndex.X:

                        if (EIndex.Y == _PlayerIndex.Y)
                        {
                            // L'ennemi est à gauche du joueur
                            EnemyDirection = DIRECTION.RIGHT;
                            Debug.WriteLine("Ennemi va à DROITE");
                        }
                        else if (EIndex.Y > _PlayerIndex.Y)
                        {
                            // Le joueur est en haut à droite

                            // Générez un nombre aléatoire entre 0 et 2 soit la valeur UP ou RIGHT
                            int RandRIGHTUP = randMaxChose * 2;
                            // Convertissez le nombre aléatoire en une valeur enum.
                            EnemyDirection = (DIRECTION)RandRIGHTUP;
                            Debug.WriteLine("Ennemi va à DROITE/HAUT");
                        }
                        else if (EIndex.Y < _PlayerIndex.Y)
                        {
                            // Le joueur est en bas à droite

                            // Générez un nombre aléatoire entre 1 et 3 soit la valeur UP ou RIGHT
                            int RandRIGHTDOWN = randMaxChose + 1;
                            // Convertissez le nombre aléatoire en une valeur enum.
                            EnemyDirection = (DIRECTION)RandRIGHTDOWN;
                        }
                        break;
                    case Vector2 EIndex when EIndex.X == _PlayerIndex.X:
                        // L'ennemi est sur la même colonne que le joueur.
                        if (EIndex.Y > _PlayerIndex.Y)
                        {
                            // L'ennemi est en dessous du joueur
                            EnemyDirection = DIRECTION.UP;
                            Debug.WriteLine("Ennemi MONTE");
                        }
                        else if (EIndex.Y < _PlayerIndex.Y)
                        {
                            // L'ennemi est au-dessus du joueur
                            EnemyDirection = DIRECTION.DOWN;
                            Debug.WriteLine("Ennemi DESCEND");
                        }
                        break;
                    default:
                        Debug.WriteLine("Position en dehors du jeu de test définit");
                        break;
                }
            }

            if (m_EntityIndex.Y == _PlayerIndex.Y++ || m_EntityIndex.X == _PlayerIndex.X++
                || m_EntityIndex.Y == _PlayerIndex.Y-- || m_EntityIndex.X == _PlayerIndex.X--)
            {
                this.Attack(ref Player);
                Debug.WriteLine("L'ennemie a attaqué le joueur");
                Debug.WriteLine("Health Point player : " + Player.GetHealthPoint());
            }

            return EnemyDirection;
        }

        /// <summary>
        /// Permet de définir où va se diriger l'enemie dans une grid selon l'emplacement du joueur. 
        /// </summary>
        /// <param name="_PlayerIndex"></param>
        /// <param name="_GridOfCase"></param>
        /// <returns>La direction de l'ennemie</returns>
        DIRECTION MovementDecisionOld(Vector2 _PlayerIndex, Case[][] _GridOfCase)
        {
            Entity Player = (Player)_GridOfCase[(int)_PlayerIndex.X][(int)_PlayerIndex.Y].GetContent();
            Vector2 curr_PlayerIndex = Player.GetIndex();//Récupère la position dans l'index du joueur

            _PlayerIndex.X -= 0.5f;
            _PlayerIndex.Y -= 0.5f;

            DIRECTION EnemyDirection = DIRECTION.NONE;

            if (m_EntityIndex.X == _PlayerIndex.X && m_EntityIndex.Y == _PlayerIndex.Y)
            {
                //Le joueur est sur la meme position que l'ennemie
                Debug.WriteLine("BACKROOM");

            }
            else
            {
                Random random = new Random();
                int randMaxChose = random.Next(2);

                if (m_EntityIndex.X == _PlayerIndex.X)
                {
                    // L'ennemi est sur la même colonne que le joueur.
                    if (m_EntityIndex.Y > _PlayerIndex.Y)
                    {
                        // L'ennemi est en dessous du joueur
                        EnemyDirection = DIRECTION.UP;
                        Debug.WriteLine("Ennemi MONTE");
                    }
                    else if (m_EntityIndex.Y < _PlayerIndex.Y)
                    {
                        // L'ennemi est au-dessus du joueur
                        EnemyDirection = DIRECTION.DOWN;
                        Debug.WriteLine("Ennemi DESCEND");
                    }
                }
                else if (m_EntityIndex.Y == _PlayerIndex.Y)
                {
                    // L'ennemi est sur la même ligne que le joueur
                    if (m_EntityIndex.X > _PlayerIndex.X)
                    {
                        // L'ennemi est à droite du joueur
                        EnemyDirection = DIRECTION.LEFT;
                        Debug.WriteLine("Ennemi va à GAUCHE");
                    }
                    else if (m_EntityIndex.X < _PlayerIndex.X)
                    {
                        // L'ennemi est à gauche du joueur
                        EnemyDirection = DIRECTION.RIGHT;
                        Debug.WriteLine("Ennemi va à DROITE");
                    }
                }
                else if (m_EntityIndex.X < _PlayerIndex.X && m_EntityIndex.Y > _PlayerIndex.Y)
                {
                    // Le joueur est en haut à droite

                    // Générez un nombre aléatoire entre 0 et 2 soit la valeur UP ou RIGHT
                    int RandRIGHTUP = randMaxChose * 2;
                    // Convertissez le nombre aléatoire en une valeur enum.
                    EnemyDirection = (DIRECTION)RandRIGHTUP;
                }
                else if (m_EntityIndex.X > _PlayerIndex.X && m_EntityIndex.Y > _PlayerIndex.Y)
                {
                    // Le joueur est en haut à gauche

                    // Générez un nombre aléatoire entre 0 et 2 soit la valeur UP ou RIGHT
                    int RandLEFTUP = randMaxChose * 3;
                    // Convertissez le nombre aléatoire en une valeur enum.
                    EnemyDirection = (DIRECTION)RandLEFTUP;
                }
                else if (m_EntityIndex.X > _PlayerIndex.X && m_EntityIndex.Y < _PlayerIndex.Y)
                {
                    // Le joueur est en bas à gauche

                    // Générez un nombre aléatoire entre 1 et 3 soit la valeur UP ou RIGHT
                    int RandLEFTDOWN = randMaxChose * 2 + 1;
                    // Convertissez le nombre aléatoire en une valeur enum.
                    EnemyDirection = (DIRECTION)RandLEFTDOWN;
                }
                else if (m_EntityIndex.X < _PlayerIndex.X && m_EntityIndex.Y < _PlayerIndex.Y)
                {
                    // Le joueur est en bas à droite

                    // Générez un nombre aléatoire entre 1 et 3 soit la valeur UP ou RIGHT
                    int RandRIGHTDOWN = randMaxChose + 1;
                    // Convertissez le nombre aléatoire en une valeur enum.
                    EnemyDirection = (DIRECTION)RandRIGHTDOWN;
                }
            }

            return EnemyDirection;
        }


        public void Update(GameTime _GameTime,
                Case[][] _GridOfCase
                )
        {
            this.Move(_GridOfCase);

        }
    }
}

