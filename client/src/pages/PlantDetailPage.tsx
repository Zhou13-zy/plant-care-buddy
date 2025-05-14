import React, { useEffect, useState } from 'react';
import { useParams, Link, useNavigate } from 'react-router-dom';
import { Plant } from '../models/Plant/plant';
import { CareEvent } from '../models/CareEvent/careEvent';
import { deletePlant, getPlantById } from '../api/plantService';
import { getCareEventsByPlant } from '../api/careEventService';
import { getHealthStatusName, getHealthStatusClass } from '../utils/healthStatusUtils';
import CareEventList from '../components/care/CareEventList';
import CareEventForm from '../components/care/CareEventForm';
import Modal from '../components/common/Modal';
import './PlantDetailPage.css';

const PlantDetailPage: React.FC = () => {
  const { id } = useParams<{ id: string }>();
  const [plant, setPlant] = useState<Plant | null>(null);
  const [careEvents, setCareEvents] = useState<CareEvent[]>([]);
  const [loading, setLoading] = useState(true);
  const [isAddEventModalOpen, setIsAddEventModalOpen] = useState(false);

  const navigate = useNavigate();

  const fetchPlantData = async () => {
    if (!id) return;
    
    try {
      const plantData = await getPlantById(id);
      setPlant(plantData);
    } catch (error) {
      console.error('Error fetching plant:', error);
      setPlant(null);
    }
  };

  const fetchCareEvents = async () => {
    if (!id) return;
    
    try {
      const events = await getCareEventsByPlant(parseInt(id, 10));
      setCareEvents(events);
    } catch (error) {
      console.error('Error fetching care events:', error);
      setCareEvents([]);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    const fetchData = async () => {
      setLoading(true);
      await fetchPlantData();
      await fetchCareEvents();
    };
    
    fetchData();
  }, [id]);

  const handleDelete = async () => {
    if (!plant) return;
    if (window.confirm('Are you sure you want to delete this plant?')) {
      try {
        await deletePlant(plant.id);
        alert('Plant deleted!');
        navigate('/plants');
      } catch (error) {
        console.error('Error deleting plant:', error);
        alert('Failed to delete plant. Please try again.');
      }
    }
  };

  const handleAddEventSuccess = () => {
    setIsAddEventModalOpen(false);
    fetchCareEvents();
  };

  const handleEventDeleted = () => {
    fetchCareEvents();
  };

  if (loading) return <div>Loading...</div>;
  if (!plant) return <div>Plant not found.</div>;

  return (
    <div className="plant-detail-page">
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
          <span className={`health-status health-${getHealthStatusClass(plant.healthStatus)}`}>
            {getHealthStatusName(plant.healthStatus)}
          </span>
        </p>
        {plant.notes && (
          <p><strong>Notes:</strong> {plant.notes}</p>
        )}
        <div className="plant-actions">
          <Link to={`/plants/${id}/edit`} className="edit-button">
            Edit Plant
          </Link>
          <button className="delete-button" onClick={handleDelete}>
            Delete Plant
          </button>
        </div>
      </div>

      <CareEventList 
        events={careEvents} 
        plantId={parseInt(id!, 10)}
        onEventDeleted={handleEventDeleted}
        onAddEvent={() => setIsAddEventModalOpen(true)}
      />

      <Modal
        isOpen={isAddEventModalOpen}
        onClose={() => setIsAddEventModalOpen(false)}
        title="Add Care Event"
      >
        <CareEventForm 
          plantId={parseInt(id!, 10)} 
          onSuccess={handleAddEventSuccess} 
        />
      </Modal>
    </div>
  );
};

export default PlantDetailPage;