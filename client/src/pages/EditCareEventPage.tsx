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
      <CareEventForm careEventId={id} />
    </div>
  );
};

export default EditCareEventPage;