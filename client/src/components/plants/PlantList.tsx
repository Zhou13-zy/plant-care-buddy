import React from 'react';
import './PlantList.css';
import { Link } from 'react-router-dom';
import { PlantHealthStatus } from '../../models/Plant/plantHealthStatus';
import { getHealthStatusName, getHealthStatusClass } from '../../utils/healthStatusUtils';

export interface Plant {
  id: number;
  name: string;
  species: string;
  location: string;
  healthStatus: PlantHealthStatus;
}

interface PlantsListProps {
  plants: Plant[];
}

const PlantsList: React.FC<PlantsListProps> = ({ plants }) => {
  return (
    <div className="plants-list">
      {plants.map(plant => (
        <div className="plant-card" key={plant.id}>
          <Link to={`/plants/${plant.id}`}>
            <h3>{plant.name}</h3>
          </Link>
          <p><strong>Species:</strong> {plant.species}</p>
          <p><strong>Location:</strong> {plant.location}</p>
          <div className={`plant-health ${getHealthStatusClass(plant.healthStatus)}`}>
            {getHealthStatusName(plant.healthStatus)}
          </div>
        </div>
      ))}
    </div>
  );
};

export default PlantsList;