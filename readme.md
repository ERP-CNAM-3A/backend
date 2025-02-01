

## 1. Introduction

Le projet **P Projet** a pour objectif de suivre les projets :  

- **Saisie de temps** des ressources sur projet  
- **Saisie de projets** avec association d’un client  
- **Simulation** pour évaluer la faisabilité d’honorer les commandes et livrer les clients  
- **Calcul des retards** et estimation des ressources supplémentaires nécessaires  
- **Intégration** des devis et opportunités de vente (pondérées par un pourcentage de chance)

## 2. Architecture et Modules Interconnectés

Le projet est organisé en plusieurs couches et modules complémentaires, chacun jouant un rôle précis dans l’ensemble de l’application :

- **API**  
  Le point d’entrée de l’application, développé en ASP.NET Core, expose des endpoints REST permettant d’interagir avec les différents modules.  
  Les endpoints se regroupent par domaines fonctionnels : *MiddleOffice*, *Project*, *Ressource* et *Sale*.

- **Application (UseCases)**  
  Contient la logique métier organisée en cas d’utilisation (UseCases).  
  Par exemple, la création et la mise à jour des projets, l’assignation/désassignation des ressources, la simulation de la livraison des projets et la récupération des ventes externes.

- **Domain**  
  Définit les entités métiers (telles que *Project*, *Ressource*, *Sale*) ainsi que les règles et exceptions spécifiques au domaine.

- **Infrastructure**  
  Implémente les aspects techniques et l’intégration avec des services externes ainsi que la persistance via des repositories.  
  Un service de synchronisation (*SyncProjectsService*) est également présent pour mettre à jour les projets.

L’architecture suit le principe de séparation des responsabilités et permet ainsi une interconnexion cohérente entre les modules grâce aux contrats (interfaces et DTOs).

## 3. Description des Interfaces (Entrées / Sorties)

L’API expose une série d’endpoints dont voici une description synthétique avec les spécifications techniques de leurs interfaces :

### 3.1. Module MiddleOffice

- **Endpoint** : `/meuch_map`  
  **Méthode** : `GET`  
  **Description** : Utilisé par le MiddleOffice pour enregistrer les endpoints du logiciel Projet
  **Entrées** : Aucune  
  **Sorties** :  
  - Code `200` avec une réponse (message ou données de diagnostic)

### 3.2. Module Project

Les endpoints du module *Project* gèrent la création, la mise à jour, la simulation et la synchronisation des projets.

- **Récupération de tous les projets**  
  **Endpoint** : `/Project/GetAllProjects`  
  **Méthode** : `GET`  
  **Entrées** : Aucune  
  **Sorties** :  
  
  - Code `200` avec la liste des projets au format JSON

- **Récupération d’un projet par son identifiant**  
  **Endpoint** : `/Project/GetProjectById/{id}`  
  **Méthode** : `GET`  
  **Entrées** :  
  
  - Paramètre `id` dans l’URL (UUID)  
    **Sorties** :  
  - Code `200` avec le détail du projet au format JSON

- **Simulation de la livraison de tous les projets**  
  **Endpoint** : `/Project/SimulateProjectsDelivery`  
  **Méthode** : `GET`  
  **Entrées** : Aucune  
  **Sorties** :  
  
  - Code `200` avec les résultats de simulation

- **Simulation de la livraison pour un projet précis**  
  **Endpoint** : `/Project/SimulateProjectDeliveryById/{id}`  
  **Méthode** : `GET`  
  **Entrées** :  
  
  - Paramètre `id` dans l’URL (UUID)  
    **Sorties** :  
  - Code `200` avec le résultat de simulation détaillé pour le projet concerné

- **Mise à jour d’un projet**  
  **Endpoint** : `/Project/UpdateProject/{id}`  
  **Méthode** : `PUT`  
  **Entrées** :  
  
  - Paramètre `id` dans l’URL (UUID)  
  - Corps de la requête : JSON conforme au schéma `UpdateProjectDTO`  
    - Exemple de propriété : `workDaysNeeded` (nombre à virgule)
      **Sorties** :  
  - Code `200` indiquant la réussite de la mise à jour

- **Assignation d’une ressource à un projet**  
  **Endpoint** : `/Project/AssignRessource/{id}`  
  **Méthode** : `PUT`  
  **Entrées** :  
  
  - Paramètre `id` dans l’URL (UUID du projet)  
  - Corps de la requête : JSON conforme au schéma `AssignRessourceDTO`  
    - Propriétés : `ressourceId` (entier), `from` (date-heure), `to` (date-heure)
      **Sorties** :  
  - Code `200` pour confirmer l’assignation

- **Désassignation d’une ressource d’un projet**  
  **Endpoint** : `/Project/DeassignRessource/{id}`  
  **Méthode** : `PUT`  
  **Entrées** :  
  
  - Paramètre `id` dans l’URL (UUID du projet)  
  - Corps de la requête : JSON conforme au schéma `AssignRessourceDTO` (même format que pour l’assignation)  
    **Sorties** :  
  - Code `200` indiquant la réussite de l’opération

- **Synchronisation des projets**  
  **Endpoint** : `/Project/SyncProjects`  
  **Méthode** : `POST`  
  **Entrées** : Aucune (le déclenchement peut être automatisé ou manuel via ce point)  
  **Sorties** :  
  
  - Code `200` indiquant la réussite de la synchronisation

### 3.3. Module Ressource

Les endpoints du module *Ressource* permettent de gérer les ressources disponibles et leur assignation sur les périodes définies.

- **Récupération de toutes les ressources**  
  **Endpoint** : `/Ressource/GetAllRessources`  
  **Méthode** : `GET`  
  **Entrées** : Aucune  
  **Sorties** :  
  
  - Code `200` avec la liste des ressources disponibles au format JSON

- **Recherche des ressources disponibles entre deux dates**  
  **Endpoint** : `/Ressource/GetAvailableRessourcesBetween`  
  **Méthode** : `POST`  
  **Entrées** :  
  
  - Corps de la requête : JSON conforme au schéma `AvailabilityPeriodDTO`  
    - Propriétés : `startDate` et `endDate` (dates au format date-time)
      **Sorties** :  
  - Code `200` avec la liste des ressources disponibles pour la période demandée

### 3.4. Module Sale

Le module *Sale* est principalement utilisé pour récupérer les informations des ventes externes.

- **Récupération des ventes externes**  
  **Endpoint** : `/Sale/GetExternalSales`  
  **Méthode** : `GET`  
  **Entrées** : Aucune  
  **Sorties** :  
  - Code `200` avec la liste sous forme de JSON

## 4. Formats et Conventions

- **Format des données**  
  Tous les échanges se font au format JSON. Les dates sont transmises au format ISO 8601 (date-time).  
  Les DTOs définissent des schémas stricts avec l’utilisation de `additionalProperties: false` pour garantir la validation des données.

- **Gestion des erreurs**  
  Les erreurs métier et techniques sont gérées au niveau du domaine via des exceptions spécifiques (voir les fichiers dans `Domain\Exceptions`).  
  Les contrôleurs de l’API renvoient des codes HTTP appropriés (par exemple, `400` pour une mauvaise requête, `404` si une entité n’est pas trouvée, etc.).

## 5. Conclusion

L’architecture du projet **P Projet** contient une structure claire et une séparation entre la couche d’interface (API), la logique métier (Application/UseCases) et le domaine.  
Les modules interconnectés (*Project*, *Ressource*, *Sale* et *MiddleOffice*) communiquent de manière cohérente grâce à des interfaces REST bien définies et des DTOs permettant d’assurer l’intégrité des échanges.

Le projet répond aux objectifs initiaux en assurant un suivi des projets, une simulation des ressources et des livraisons, et en intégrant des services externes. La conception orientée domaine et l’utilisation d’une architecture en couches garantissent la robustesse et la pérennité de la solution ERP.