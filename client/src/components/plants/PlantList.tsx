import React from 'react';
import './PlantList.css';
import { Link } from 'react-router-dom';
import { PlantHealthStatus } from '../../models/Plant/plantHealthStatus';
import HealthStatusIndicator from '../common/HealthStatusIndicator';

export interface Plant {
  id: string;
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
          <div className="plant-card-header">
            <Link to={`/plants/${plant.id}`}>
              <h3>{plant.name}</h3>
            </Link>
            <HealthStatusIndicator 
              status={plant.healthStatus} 
              size="small" 
            />
          </div>
          <p><strong>Species:</strong> {plant.species}</p>
          <p><strong>Location:</strong> {plant.location}</p>
        </div>
      ))}
    </div>
  );
};

export default PlantsList;