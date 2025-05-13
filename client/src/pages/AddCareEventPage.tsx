import React from 'react';
import { useParams } from 'react-router-dom';
import CareEventForm from '../components/care/CareEventForm';

const AddCareEventPage: React.FC = () => {
  const { plantId } = useParams<{ plantId: string }>();
  
  return (
    <div>
      <h1>Add Care Event</h1>
      <CareEventForm plantId={plantId ? parseInt(plantId, 10) : undefined} />
    </div>
  );
};

export default AddCareEventPage;