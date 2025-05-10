import React from 'react';
import './PlantList.css';

export interface Plant {
  id: number;
  name: string;
  species: string;
  location: string;
  healthStatus: string;
}

interface PlantsListProps {
  plants: Plant[];
}

const PlantsList: React.FC<PlantsListProps> = ({ plants }) => {
  return (
    <div className="plants-list">
      {plants.map(plant => (
        <div className="plant-card" key={plant.id}>
          <h3>{plant.name}</h3>
          <p><strong>Species:</strong> {plant.species}</p>
          <p><strong>Location:</strong> {plant.location}</p>
          <p><strong>Health:</strong> {plant.healthStatus}</p>
        </div>
      ))}
    </div>
  );
};

export default PlantsList;