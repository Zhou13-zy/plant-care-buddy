import React, { useEffect, useState } from 'react';
import { useParams, Link, useNavigate } from 'react-router-dom';
import { Plant } from '../models/plant';
import { deletePlant, getPlantById } from '../api/plantService';
import './PlantDetailPage.css';

const PlantDetailPage: React.FC = () => {
  const { id } = useParams<{ id: string }>();
  const [plant, setPlant] = useState<Plant | null>(null);
  const [loading, setLoading] = useState(true);

  const navigate = useNavigate();

  const handleDelete = async () => {
    if (!plant) return;
    if (window.confirm('Are you sure you want to delete this plant?')) {
      await deletePlant(plant.id);
      alert('Plant deleted!');
      navigate('/plants');
    }
  };

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
      <Link to={`/plants/${id}/edit`} className="edit-button">
        Edit Plant
      </Link>
      <button className="delete-button" onClick={handleDelete}>
        Delete Plant
      </button>
    </div>
  );
};

export default PlantDetailPage;