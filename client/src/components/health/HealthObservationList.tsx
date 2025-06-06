import React from 'react';
import { useNavigate } from 'react-router-dom';
import { HealthObservation } from '../../models/HealthObservation/healthObservation';
import { deletePlant } from '../../api/healthObservationService';
import { formatDate } from '../../utils/dateUtils';
import { getHealthStatusName, getHealthStatusClass } from '../../utils/healthStatusUtils';
import './HealthObservationList.css';
import ImageDisplay from '../common/ImageDisplay';

interface HealthObservationListProps {
  observations: HealthObservation[];
  plantId: string;
  onObservationDeleted: () => void;
  onAddObservation?: () => void;
}

const HealthObservationList: React.FC<HealthObservationListProps> = ({ 
  observations, 
  plantId, 
  onObservationDeleted,
  onAddObservation
}) => {
  const navigate = useNavigate();
  
  const handleAddObservation = () => {
    navigate(`/plants/${plantId}/health-observations/new`);
  };

  const handleEditObservation = (observationId: string) => {
    navigate(`/health-observations/${observationId}/edit`);
  };

  const handleDeleteObservation = async (observationId: string) => {
    if (window.confirm('Are you sure you want to delete this health observation?')) {
      try {
        await deletePlant(observationId);
        // Call the callback to notify parent component
        onObservationDeleted();
      } catch (err) {
        console.error('Failed to delete health observation:', err);
        alert('Failed to delete health observation. Please try again.');
      }
    }
  };

  return (
    <div className="health-observation-list-container">
      <div className="observation-header">
        <h2>Health Observations</h2>
        <button 
          className="add-observation-button"
          onClick={onAddObservation}
        >
          Add Observation
        </button>
      </div>

      {observations.length === 0 ? (
        <div className="no-observations">
          No health observations recorded yet. Click "Add Observation" to create one.
        </div>
      ) : (
        <div className="observation-list">
          {observations.map((observation) => (
            <div key={observation.id} className="observation-card">
              <div className="observation-info">
                <div className="observation-date">
                  {formatDate(observation.observationDate)}
                </div>
                <div className={`health-status-badge ${getHealthStatusClass(observation.healthStatus)}`}>
                  {getHealthStatusName(observation.healthStatus)}
                </div>
              </div>
              <div className="observation-details">
                <p className="observation-notes">{observation.notes}</p>
                {observation.imagePath && (
                  <div className="observation-image">
                    <ImageDisplay 
                      imagePath={observation.imagePath} 
                      alt="Health observation" 
                      style={{ maxWidth: '100%', maxHeight: '180px', borderRadius: '6px' }}
                    />
                  </div>
                )}
              </div>
              <div className="observation-actions">
                <button 
                  className="edit-button"
                  onClick={() => handleEditObservation(observation.id)}
                >
                  Edit
                </button>
                <button 
                  className="delete-button"
                  onClick={() => handleDeleteObservation(observation.id)}
                >
                  Delete
                </button>
              </div>
            </div>
          ))}
        </div>
      )}
    </div>
  );
};

export default HealthObservationList;