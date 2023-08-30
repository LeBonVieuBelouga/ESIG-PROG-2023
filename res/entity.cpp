/**
  \file
  \brief    Définition de la classe Entity.
  @author   Léo Küttel
  @date     Janvier 2022
 */
#include "entity.h"

#include <cmath>
#include <QDebug>

#include "sprite.h"
#include "gamescene.h"
#include "gamecanvas.h"
#include "resources.h"

const int MIN_INTERSECTED = 30;      //  Intersection minimum

//!
//! \param rPixmap
//! \param pParent
//! \param _spawnPoint point d'apparition de l'entité.
Entity::Entity(const QPixmap& rPixmap, QGraphicsItem* pParent) : Sprite(rPixmap,pParent)
{

}

//! Premet de définir le point d'apparition de l'entité dans la scène.
//! \param _spawnPoint nouveau point d'apparition.
//!
void Entity::setSpawnPoint(QPointF _spawnPoint){
    m_spawnPoint = _spawnPoint;
}

//! Premet de récupéré le point d'apparition de l'entité dans la scène.
//!
QPointF Entity::getSpawnPoint(){
    return m_spawnPoint;
}

///!Permet de configurer l'animation d'une entité
//!
void Entity::configureAnimation(){

}

//! Permet de savoir si l'entité touche le sol
//! \return un booléane
//!
bool Entity::getIsOnFloor(){
    return this->m_isOnFloor;
}

//! Permet de définir si l'entité touche le sol ou non
//! \param _isOnFloor  la nouvelle valeur de m_isOnFloor
//!
void Entity::setIsOnFloor(bool _isOnFloor){
    this->m_isOnFloor = _isOnFloor;
}

//! Permet de s'avoir si l'entité est morte
//! \return un booléane
//!
bool Entity::getIsDeath(){
    return this->m_isDeath;
}

//! Permet de définir si l'entité doit périre ou non.
//! \param _isDeath la nouvelle valeur de m_isDeath.
//!
void Entity::setIsDeath(bool _isDeath){
    this->m_isDeath = _isDeath;
}

//! Ajoute à une liste la localisation des collision entre l'entité et un sprite.
//! \param collisionLocateList
//! \param posSprite Position du Sprite principal.
//! \param intersected zone de collision entre les deux sprites.
void Entity::getCollisionLocate(QList<Entity::hitSide>&collisionLocateL,
                                QRectF posSprite,QRectF intersected){

    //Si l'intersected est plus large la collision est vertical.
    if (intersected.width() > intersected.height() && intersected.width() > MIN_INTERSECTED) {
        if (intersected.center().y() < posSprite.center().y())
            //Détermine le haut
            Entity::uniqueSide(&collisionLocateL, Entity::hitSide::UP);
        else
            //Détermine le bas
            Entity::uniqueSide(&collisionLocateL, Entity::hitSide::DOWN);

        //Sinon si la collision est plus haut que large est horizontal.
    } else if (intersected.width() < intersected.height() && intersected.height() > MIN_INTERSECTED){
        if (intersected.center().x() < posSprite.center().x())
            //Détermine la gauche
            Entity::uniqueSide(&collisionLocateL, Entity::hitSide::LEFT);
        else
            //Détermine la droite
            Entity::uniqueSide(&collisionLocateL, Entity::hitSide::RIGHT);
    }

}

//! Permet de rendre les éléments de la liste unique
//! \param collidingSidesList liste à testé
//! \param appendToSide élément à apprendre
//!
void Entity::uniqueSide(QList<hitSide>* collidingSidesList, hitSide appendToSide){
    if (!collidingSidesList->contains(appendToSide)) {
        collidingSidesList->append(appendToSide);
    }
}

//! Applique une force d'attraction vers le bas de l'écran à une entitié.
//! \param entity sprite au quel on applique la gravité
//! \param enti_velocity velocité du sprite
//! \param elapsedTime temps écoulé entre chaque tick.
void Entity::gravityApplied(long long elapsedTime){
    if (!this->getIsOnFloor()){
        if(this->m_velocity.y() <= 0.3 && this->m_velocity.y() >= 0)
            this->setIsOnFloor(true);
        else
            this->setIsOnFloor(false);

        this->m_velocity += m_gravity * (elapsedTime/100.0);
    }
}

//! Définit la scene sur la quel l'entité est liée.
//! \param newScene La nouvelle scene définit .
void Entity::setScene(GameScene* newScene){
    m_pScene = newScene;

}
