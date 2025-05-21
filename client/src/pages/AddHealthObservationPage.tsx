import React from 'react';
import { useParams } from 'react-router-dom';
import HealthObservationForm from '../components/health/HealthObservationForm';
import './AddHealthObservationPage.css';

const AddHealthObservationPage: React.FC = () => {
  const { plantId } = useParams<{ plantId: string }>();
  
  return (
    <div className="add-health-observation-page-container">
      <HealthObservationForm 
        plantId={plantId ?? undefined} 
      />
    </div>
  );
};

export default AddHealthObservationPage;