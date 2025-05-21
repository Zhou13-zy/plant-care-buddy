import React, { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import HealthObservationForm from '../components/health/HealthObservationForm';
import { getHealthObservation } from '../api/healthObservationService';
import { HealthObservation } from '../models/HealthObservation/healthObservation';
import { formatDate } from '../utils/dateUtils';

const EditHealthObservationPage: React.FC = () => {
  const { observationId } = useParams<{ observationId: string }>();
  const navigate = useNavigate();
  const [initialData, setInitialData] = useState<HealthObservation | null>(null);
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    const fetchObservation = async () => {
      if (!observationId) return;
      try {
        const observation = await getHealthObservation(observationId);
        setInitialData(observation);
      } catch (error) {
        console.error('Error fetching health observation:', error);
        alert('Failed to load health observation data');
      } finally {
        setIsLoading(false);
      }
    };

    fetchObservation();
  }, [observationId]);

  if (isLoading) return <div>Loading...</div>;
  if (!initialData) return <div>Health observation not found</div>;

  return (
    <div className="container">
      <HealthObservationForm 
        observationId={observationId}
        initialData={initialData}
      />
    </div>
  );
};

export default EditHealthObservationPage;