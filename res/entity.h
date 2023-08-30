#ifndef ENTITY_H
#define ENTITY_H

#include <QObject>
#include <QPointF>

#include <QGraphicsTransform>
#include <QList>

#include <sprite.h>

//!
//! \brief The Entity class
//! Cette classe est permet de crée une entité et de la géré avec différente méthode.
//! Une entité est un sprite qui à des fonctions qui lui son propre elle peut avoir pluiseur aniamtion,une velocité
//! qui lui est propre.
//! \section1 Déplacement d'une entité
//! Une entité peut se mouvoire dans un espace donnée(la scene sur la quelle elle a été assigné).
//! Ses déplacement et collision sont géré par les sous classe de celle-ci qui réutiliseront les sous classe de \ref EntityTickHandler.
//! \section2 Mort de l'entité
//! La mort d'une entité consite à renvoyé un booléan qui redéfinit sont état et le détruit de la scene sur la quel il apparait.
//! \section3 Collision ciblé
//! Cette classe permet de récupéré les collision ciblé que le sprite va avoir.
//! Elle permet de localisé l'endroit exacte ou elle a été touché.
class Entity: public Sprite
{
public:

    Entity(const QPixmap& rPixmap, QGraphicsItem* pParent = nullptr);

    enum hitSide{
        UP =0,
        DOWN =1,
        RIGHT=2,
        LEFT =3
    };

    virtual void configureAnimation();

    //Gestion des déplacement
    QPointF m_velocity;
    QPointF m_lastVelocity = m_velocity;
    QPointF getVelocity();

    bool getIsOnFloor();
    virtual void setIsOnFloor(bool _isOnFloor);

    QPointF m_spawnPoint = QPointF(0,0);
    void setSpawnPoint(QPointF _spawnPoint);
    QPointF getSpawnPoint();


    bool getIsDeath();
    virtual void setIsDeath(bool _isDeath);


    void setScene(GameScene* m_pScene);


    static void uniqueSide(QList<hitSide>* collidingSidesList, hitSide appendToSide);

    void getCollisionLocate(QList<Entity::hitSide>&collisionLocateL,
                                      QRectF posSprite,QRectF intersected);
    void gravityApplied(long long elapsedTime);

private:


protected:
    GameScene* m_pScene = nullptr;
    QPointF m_gravity = QPointF(0,2);
    bool m_isDeath = false;
    bool m_isOnFloor = false;
};

#endif // ENTITY_H
