import React, { useEffect, useState, useCallback } from 'react';
import { useParams, Link, useNavigate } from 'react-router-dom';
import { Plant } from '../models/Plant/plant';
import { CareEvent } from '../models/CareEvent/careEvent';
import { HealthObservation } from '../models/HealthObservation/healthObservation';
import { deletePlant, getPlantById } from '../api/plantService';
import { getCareEventsByPlant } from '../api/careEventService';
import { getHealthObservationsByPlantId } from '../api/healthObservationService';
import { generateRemindersForPlant, getRemindersByPlantId } from '../api/reminderService';
import CareEventList from '../components/care/CareEventList';
import CareEventForm from '../components/care/CareEventForm';
import Modal from '../components/common/Modal';
import HealthObservationList from '../components/health/HealthObservationList';
import HealthStatusIndicator from '../components/common/HealthStatusIndicator';
import ImageDisplay from '../components/common/ImageDisplay';
import './PlantDetailPage.css';
import { getPlantTypeName } from '../utils/plantTypeUtils';
import PlantCareRecommendations from '../components/care/PlantCareRecommendations';
import HealthObservationForm from '../components/health/HealthObservationForm';
import RemindersView from '../components/dashboard/RemindersView';
import { Reminder } from '../models/Reminder/reminder';

const PlantDetailPage: React.FC = () => {
  const { id } = useParams<{ id: string }>();
  const [plant, setPlant] = useState<Plant | null>(null);
  const [careEvents, setCareEvents] = useState<CareEvent[]>([]);
  const [healthObservations, setHealthObservations] = useState<HealthObservation[]>([]);
  const [loading, setLoading] = useState(true);
  const [isAddEventModalOpen, setIsAddEventModalOpen] = useState(false);
  const [isAddObservationModalOpen, setIsAddObservationModalOpen] = useState(false);
  const [generating, setGenerating] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [reminders, setReminders] = useState<Reminder[]>([]);
  const [remindersLoading, setRemindersLoading] = useState(true);

  const navigate = useNavigate();

  const fetchPlantData = useCallback(async () => {
    if (!id) return;
    
    try {
      const plantData = await getPlantById(id);
      setPlant(plantData);
    } catch (error) {
      console.error('Error fetching plant:', error);
      setPlant(null);
    }
  }, [id]);

  const fetchCareEvents = useCallback(async () => {
    if (!id) return;
    
    try {
      const events = await getCareEventsByPlant(id);
      setCareEvents(events);
    } catch (error) {
      console.error('Error fetching care events:', error);
      setCareEvents([]);
    }
  }, [id]);

  const fetchHealthObservations = useCallback(async () => {
    if (!id) return;
    
    try {
      const observations = await getHealthObservationsByPlantId(id);
      setHealthObservations(observations);
    } catch (error) {
      console.error('Error fetching health observations:', error);
      setHealthObservations([]);
    } finally {
      setLoading(false);
    }
  }, [id]);

  const fetchReminders = useCallback(async () => {
    if (!id) return;
    setRemindersLoading(true);
    try {
      const data = await getRemindersByPlantId(id);
      setReminders(data);
    } catch {
      setReminders([]);
    } finally {
      setRemindersLoading(false);
    }
  }, [id]);

  useEffect(() => {
    const fetchData = async () => {
      setLoading(true);
      await fetchPlantData();
      await fetchCareEvents();
      await fetchHealthObservations();
      await fetchReminders();
    };
    
    fetchData();
  }, [id, fetchPlantData, fetchCareEvents, fetchHealthObservations, fetchReminders]);

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
  
  const handleHealthObservationDeleted = () => {
    fetchHealthObservations();
  };

  const handleAddObservationSuccess = () => {
    setIsAddObservationModalOpen(false);
    fetchHealthObservations();
  };

  const handleGenerateReminders = async () => {
    setGenerating(true);
    setError(null);
    try {
      await generateRemindersForPlant(plant!.id);
      await fetchReminders();
    } catch (err) {
      setError('Failed to generate reminders. Please try again.');
    } finally {
      setGenerating(false);
    }
  };

  if (loading) return <div>Loading...</div>;
  if (!plant) return <div>Plant not found.</div>;
  if (!id) return <div>Invalid plant ID.</div>;

  return (
    <div className="plant-detail-page">
      <div className="plant-detail-top">
        <div className="plant-detail-card">
          <h1>{plant.name}</h1>
          {plant.primaryImagePath && (
            <div className="plant-photo">
              <ImageDisplay
                imagePath={plant.primaryImagePath}
                alt={plant.name}
                style={{ maxWidth: '100%', maxHeight: '340px', borderRadius: '12px' }}
              />
            </div>
          )}
          <p><strong>Species:</strong> {plant.species}</p>
          <p><strong>Plant Type:</strong> {getPlantTypeName(plant.plantType)}</p>
          <p><strong>Location:</strong> {plant.location}</p>
          <p><strong>Acquisition Date:</strong> {plant.acquisitionDate}</p>
          <p>
            <strong>Health Status:</strong>
            <HealthStatusIndicator status={plant.healthStatus} size="medium" />
          </p>
          <p><strong>Next Health Check Date:</strong> {plant.nextHealthCheckDate}</p>
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
        <div className="plant-care-card">
          <h2>Care Recommendations</h2>
          <PlantCareRecommendations plantId={id as string} />
        </div>
      </div>

      <div className="plant-section">
        <HealthObservationList 
          observations={healthObservations}
          plantId={id as string}
          onObservationDeleted={handleHealthObservationDeleted}
          onAddObservation={() => setIsAddObservationModalOpen(true)}
        />
      </div>

      <div className="plant-section">
        <h2>Care History</h2>
        <CareEventList 
          events={careEvents} 
          plantId={id as string}
          onEventDeleted={handleEventDeleted}
          onAddEvent={() => setIsAddEventModalOpen(true)}
        />
      </div>

      <div className="reminders-section">
        <div className="reminders-header">
          <h2>Reminders</h2>
          <button
            className="primary-action"
            onClick={handleGenerateReminders}
            disabled={generating}
          >
            {generating ? "Generating..." : "Generate Recommended Reminders"}
          </button>
        </div>
        {error && <div className="error-message">{error}</div>}
        <RemindersView reminders={reminders} loading={remindersLoading} />
      </div>

      <Modal
        isOpen={isAddEventModalOpen}
        onClose={() => setIsAddEventModalOpen(false)}
        title="Add Care Event"
      >
        <CareEventForm 
          plantId={id} 
          onSuccess={handleAddEventSuccess} 
        />
      </Modal>

      <Modal
        isOpen={isAddObservationModalOpen}
        onClose={() => setIsAddObservationModalOpen(false)}
        title="Add Health Observation"
      >
        <HealthObservationForm 
          plantId={id}
          onSuccess={handleAddObservationSuccess}
        />
      </Modal>
    </div>
  );
};

export default PlantDetailPage;