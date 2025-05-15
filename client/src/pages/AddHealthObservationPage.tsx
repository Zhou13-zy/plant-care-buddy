import React from 'react';
import { useParams } from 'react-router-dom';
import HealthObservationForm from '../components/health/HealthObservationForm';

const AddHealthObservationPage: React.FC = () => {
  const { plantId } = useParams<{ plantId: string }>();
  
  return (
    <div className="container">
      <HealthObservationForm 
        plantId={plantId ? parseInt(plantId, 10) : undefined} 
      />
    </div>
  );
};

export default AddHealthObservationPage;