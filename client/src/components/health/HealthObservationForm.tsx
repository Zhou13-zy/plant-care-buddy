import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { PlantHealthStatus } from '../../models/Plant/plantHealthStatus';
import { CreateHealthObservationDto } from '../../models/HealthObservation/createHealthObservationDto';
import { UpdateHealthObservationDto } from '../../models/HealthObservation/updateHealthObservationDto';
import { 
  createHealthObservation, 
  updateHealthObservation 
} from '../../api/healthObservationService';
import ImageUpload from '../common/ImageUpload';
import './HealthObservationForm.css';
import { HealthObservation } from '../../models/HealthObservation/healthObservation';
import { formatDate } from '../../utils/dateUtils';

interface HealthObservationFormProps {
  plantId?: string; // Optional: passed when creating an observation for a specific plant
  observationId?: string; // Optional: passed when editing an existing observation
  onSuccess?: () => void; // Optional callback after successful submission
  initialData?: HealthObservation;  // Add this
}

const HealthObservationForm: React.FC<HealthObservationFormProps> = ({ 
  plantId, 
  observationId, 
  onSuccess,
  initialData 
}) => {
  const navigate = useNavigate();
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);
  const [selectedPhoto, setSelectedPhoto] = useState<File | null>(null);
  const [formData, setFormData] = useState<{
    plantId: string;
    observationDate: string;
    healthStatus: PlantHealthStatus;
    notes: string;
    imagePath?: string;
  }>({
    plantId: initialData?.plantId || plantId || '',
    observationDate: initialData ? formatDate(initialData.observationDate) : new Date().toISOString().split('T')[0],
    healthStatus: initialData?.healthStatus || PlantHealthStatus.Healthy,
    notes: initialData?.notes || '',
    imagePath: initialData?.imagePath || ''
  });

  // Determine if we're in edit mode
  const isEditMode = !!observationId;

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
        const updateDto: UpdateHealthObservationDto = {
          observationDate: formData.observationDate,
          healthStatus: formData.healthStatus,
          notes: formData.notes,
          photo: selectedPhoto || undefined
        };
        await updateHealthObservation(observationId, updateDto);
      } else {
        const createDto: CreateHealthObservationDto = {
          plantId: formData.plantId,
          observationDate: formData.observationDate,
          healthStatus: formData.healthStatus,
          notes: formData.notes,
          photo: selectedPhoto || undefined
        };
        await createHealthObservation(createDto);
      }

      setIsLoading(false);
      if (onSuccess) {
        onSuccess();
      } else {
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
              type="string"
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
          <label htmlFor="photo">Photo:</label>
          <ImageUpload
            onFileSelect={(file) => {
              setSelectedPhoto(file);
            }}
            initialUrl={isEditMode ? formData.imagePath : undefined}
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