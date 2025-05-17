import React from 'react';
import { useParams } from 'react-router-dom';
import HealthObservationForm from '../components/health/HealthObservationForm';

const EditHealthObservationPage: React.FC = () => {
  const { observationId } = useParams<{ observationId: string }>();
  
  return (
    <div className="container">
      <HealthObservationForm 
        observationId={observationId ? parseInt(observationId, 10) : undefined} 
      />
    </div>
  );
};

export default EditHealthObservationPage;