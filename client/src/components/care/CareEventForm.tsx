import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { CareEventType } from '../../models/CareEvent/careEventType';
import { CreateCareEventDto } from '../../models/CareEvent/createCareEventDto';
import { UpdateCareEventDto } from '../../models/CareEvent/updateCareEventDto';
import { createCareEvent, getCareEventById, updateCareEvent } from '../../api/careEventService';
import { getCareEventTypeOptions } from '../../utils/careEventUtils';
import './CareEventForm.css';

interface CareEventFormProps {
  plantId?: number; // Optional: passed when creating a care event for a specific plant
  careEventId?: number; // Optional: passed when editing an existing care event
  onSuccess?: () => void; // Optional callback after successful submission
}

const CareEventForm: React.FC<CareEventFormProps> = ({ plantId, careEventId, onSuccess }) => {
  const navigate = useNavigate();
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);
  const [formData, setFormData] = useState<{
    plantId: number;
    eventType: CareEventType;
    eventDate: string;
    notes: string;
    imagePath: string;
  }>({
    plantId: plantId || 0,
    eventType: CareEventType.Watering,
    eventDate: new Date().toISOString().split('T')[0], // Default to today
    notes: '',
    imagePath: ''
  });

  // Determine if we're in edit mode
  const isEditMode = !!careEventId;

  // Load care event data if in edit mode
  useEffect(() => {
    if (isEditMode && careEventId) {
      setIsLoading(true);
      getCareEventById(careEventId)
        .then(careEvent => {
          setFormData({
            plantId: careEvent.plantId,
            eventType: careEvent.eventType,
            eventDate: new Date(careEvent.eventDate).toISOString().split('T')[0],
            notes: careEvent.notes || '',
            imagePath: careEvent.imagePath || ''
          });
          setIsLoading(false);
        })
        .catch(err => {
          setError('Error loading care event data. Please try again.');
          setIsLoading(false);
          console.error('Failed to load care event:', err);
        });
    }
  }, [careEventId, isEditMode]);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement | HTMLTextAreaElement>) => {
    const { name, value } = e.target;
    setFormData(prev => ({
      ...prev,
      [name]: name === 'eventType' || name === 'plantId' ? parseInt(value, 10) : value
    }));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setIsLoading(true);
    setError(null);

    try {
      if (isEditMode && careEventId) {
        // Update existing care event
        const updateDto: UpdateCareEventDto = {
          eventType: formData.eventType,
          eventDate: formData.eventDate,
          notes: formData.notes || undefined,
          imagePath: formData.imagePath || undefined
        };
        await updateCareEvent(careEventId, updateDto);
      } else {
        // Create new care event
        const createDto: CreateCareEventDto = {
          plantId: formData.plantId,
          eventType: formData.eventType,
          eventDate: formData.eventDate,
          notes: formData.notes || undefined,
          imagePath: formData.imagePath || undefined
        };
        await createCareEvent(createDto);
      }

      setIsLoading(false);
      if (onSuccess) {
        onSuccess();
      } else {
        // Navigate back or to appropriate page
        navigate(-1);
      }
    } catch (err) {
      setError('Failed to save care event. Please try again.');
      setIsLoading(false);
      console.error('Error saving care event:', err);
    }
  };

  // Generate event type options from enum
  const eventTypeOptions = getCareEventTypeOptions().map(option => (
    <option key={option.value} value={option.value}>
      {option.label}
    </option>
  ));

  if (isLoading && isEditMode) {
    return <div className="loading">Loading care event data...</div>;
  }

  return (
    <div className="care-event-form-container">
      <h2>{isEditMode ? 'Edit Care Event' : 'Add Care Event'}</h2>
      {error && <div className="error-message">{error}</div>}
      
      <form onSubmit={handleSubmit} className="care-event-form">
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
          <label htmlFor="eventType">Event Type:</label>
          <select
            id="eventType"
            name="eventType"
            value={formData.eventType}
            onChange={handleChange}
            required
          >
            {eventTypeOptions}
          </select>
        </div>

        <div className="form-group">
          <label htmlFor="eventDate">Event Date:</label>
          <input
            type="date"
            id="eventDate"
            name="eventDate"
            value={formData.eventDate}
            onChange={handleChange}
            required
          />
        </div>

        <div className="form-group">
          <label htmlFor="notes">Notes:</label>
          <textarea
            id="notes"
            name="notes"
            value={formData.notes}
            onChange={handleChange}
            rows={4}
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

export default CareEventForm;