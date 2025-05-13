import React from 'react';
import { useParams } from 'react-router-dom';
import CareEventForm from '../components/care/CareEventForm';

const EditCareEventPage: React.FC = () => {
  const { id } = useParams<{ id: string }>();
  
  if (!id) {
    return <div>Care event ID is required</div>;
  }
  
  return (
    <div>
      <h1>Edit Care Event</h1>
      <CareEventForm careEventId={parseInt(id, 10)} />
    </div>
  );
};

export default EditCareEventPage;