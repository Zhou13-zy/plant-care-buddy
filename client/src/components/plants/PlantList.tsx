import React from 'react';
import './PlantList.css';
import { Link } from 'react-router-dom';

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
          <Link to={`/plants/${plant.id}`}>
            <h3>{plant.name}</h3>
          </Link>
          <p><strong>Species:</strong> {plant.species}</p>
          <p><strong>Location:</strong> {plant.location}</p>
          <p><strong>Health:</strong> {plant.healthStatus}</p>
        </div>
      ))}
    </div>
  );
};

export default PlantsList;