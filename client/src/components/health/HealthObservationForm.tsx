import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { PlantHealthStatus } from '../../models/Plant/plantHealthStatus';
import { CreateHealthObservationDto } from '../../models/HealthObservation/createHealthObservationDto';
import { UpdateHealthObservationDto } from '../../models/HealthObservation/updateHealthObservationDto';
import { 
  createHealthObservation, 
  getHealthObservation, 
  updateHealthObservation 
} from '../../api/healthObservationService';
import './HealthObservationForm.css';

interface HealthObservationFormProps {
  plantId?: number; // Optional: passed when creating an observation for a specific plant
  observationId?: number; // Optional: passed when editing an existing observation
  onSuccess?: () => void; // Optional callback after successful submission
}

const HealthObservationForm: React.FC<HealthObservationFormProps> = ({ 
  plantId, 
  observationId, 
  onSuccess 
}) => {
  const navigate = useNavigate();
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);
  const [formData, setFormData] = useState<{
    plantId: number;
    observationDate: string;
    healthStatus: PlantHealthStatus;
    notes: string;
    imagePath: string;
  }>({
    plantId: plantId || 0,
    observationDate: new Date().toISOString().split('T')[0], // Default to today
    healthStatus: PlantHealthStatus.Healthy,
    notes: '',
    imagePath: ''
  });

  // Determine if we're in edit mode
  const isEditMode = !!observationId;

  // Load health observation data if in edit mode
  useEffect(() => {
    if (isEditMode && observationId) {
      setIsLoading(true);
      getHealthObservation(observationId)
        .then(observation => {
          setFormData({
            plantId: observation.plantId,
            observationDate: new Date(observation.observationDate).toISOString().split('T')[0],
            healthStatus: observation.healthStatus,
            notes: observation.notes || '',
            imagePath: observation.imagePath || ''
          });
          setIsLoading(false);
        })
        .catch(err => {
          setError('Error loading health observation data. Please try again.');
          setIsLoading(false);
          console.error('Failed to load health observation:', err);
        });
    }
  }, [observationId, isEditMode]);

  const handleChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement | HTMLTextAreaElement>
  ) => {
    const { name, value } = e.target;
    setFormData(prev => ({
      ...prev,
      [name]: name === 'healthStatus' || name === 'plantId' 
        ? parseInt(value, 10) 
        : value
    }));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setIsLoading(true);
    setError(null);

    try {
      if (isEditMode && observationId) {
        // Update existing health observation
        const updateDto: UpdateHealthObservationDto = {
          observationDate: formData.observationDate,
          healthStatus: formData.healthStatus,
          notes: formData.notes,
          imagePath: formData.imagePath || undefined
        };
        await updateHealthObservation(observationId, updateDto);
      } else {
        // Create new health observation
        const createDto: CreateHealthObservationDto = {
          plantId: formData.plantId,
          observationDate: formData.observationDate,
          healthStatus: formData.healthStatus,
          notes: formData.notes,
          imagePath: formData.imagePath || undefined
        };
        await createHealthObservation(createDto);
      }

      setIsLoading(false);
      if (onSuccess) {
        onSuccess();
      } else {
        // Navigate back or to appropriate page
        navigate(-1);
      }
    } catch (err) {
      setError('Failed to save health observation. Please try again.');
      setIsLoading(false);
      console.error('Error saving health observation:', err);
    }
  };

  // Generate health status options from enum
  const healthStatusOptions = Object.entries(PlantHealthStatus)
    .filter(([key, value]) => typeof value === 'number')
    .map(([key, value]) => (
      <option key={value as number} value={value as number}>
        {key}
      </option>
    ));

  if (isLoading && isEditMode) {
    return <div className="loading">Loading health observation data...</div>;
  }

  return (
    <div className="health-observation-form-container">
      <h2>{isEditMode ? 'Edit Health Observation' : 'Add Health Observation'}</h2>
      {error && <div className="error-message">{error}</div>}
      
      <form onSubmit={handleSubmit} className="health-observation-form">
        {!isEditMode && !plantId && (
          <div className="form-group">
            <label htmlFor="plantId">Plant ID:</label>
            <input
              type="number"
              id="plantId"
              name="plantId"
              value={formData.plantId}
              onChange={handleChange}
              required
            />
          </div>
        )}

        <div className="form-group">
          <label htmlFor="observationDate">Observation Date:</label>
          <input
            type="date"
            id="observationDate"
            name="observationDate"
            value={formData.observationDate}
            onChange={handleChange}
            required
          />
        </div>

        <div className="form-group">
          <label htmlFor="healthStatus">Health Status:</label>
          <select
            id="healthStatus"
            name="healthStatus"
            value={formData.healthStatus}
            onChange={handleChange}
            required
          >
            {healthStatusOptions}
          </select>
        </div>

        <div className="form-group">
          <label htmlFor="notes">Notes:</label>
          <textarea
            id="notes"
            name="notes"
            value={formData.notes}
            onChange={handleChange}
            rows={4}
            required
          />
        </div>

        <div className="form-group">
          <label htmlFor="imagePath">Image Path:</label>
          <input
            type="text"
            id="imagePath"
            name="imagePath"
            value={formData.imagePath}
            onChange={handleChange}
          />
        </div>

        <div className="form-actions">
          <button 
            type="button" 
            onClick={() => navigate(-1)} 
            className="button-secondary"
          >
            Cancel
          </button>
          <button 
            type="submit" 
            disabled={isLoading} 
            className="button-primary"
          >
            {isLoading ? 'Saving...' : isEditMode ? 'Update' : 'Create'}
          </button>
        </div>
      </form>
    </div>
  );
};

export default HealthObservationForm;