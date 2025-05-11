import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { Plant } from '../models/plant';
import { getPlantById } from '../api/plantService';
import './PlantDetailPage.css';

const PlantDetailPage: React.FC = () => {
  const { id } = useParams<{ id: string }>();
  const [plant, setPlant] = useState<Plant | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    if (!id) return;
    getPlantById(id)
      .then(setPlant)
      .catch(() => setPlant(null))
      .finally(() => setLoading(false));
  }, [id]);

  if (loading) return <div>Loading...</div>;
  if (!plant) return <div>Plant not found.</div>;

  return (
    <div className="plant-detail">
      {plant.primaryImagePath && (
        <img
          src={plant.primaryImagePath}
          alt={plant.name}
          className="plant-detail-image"
        />
      )}
      <h1>{plant.name}</h1>
      <p><strong>Species:</strong> {plant.species}</p>
      <p><strong>Location:</strong> {plant.location}</p>
      <p><strong>Acquisition Date:</strong> {plant.acquisitionDate}</p>
      <p>
        <strong>Health Status:</strong>
        <span className={`health-status health-${plant.healthStatus.toLowerCase()}`}>
          {plant.healthStatus}
        </span>
      </p>
      {plant.notes && (
        <p><strong>Notes:</strong> {plant.notes}</p>
      )}
    </div>
  );
};

export default PlantDetailPage;