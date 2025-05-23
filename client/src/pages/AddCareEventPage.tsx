import React from 'react';
import { useParams } from 'react-router-dom';
import CareEventForm from '../components/care/CareEventForm';

const AddCareEventPage: React.FC = () => {
  const { plantId } = useParams<{ plantId: string }>();
  
  return (
    <div>
      <CareEventForm plantId={plantId ?? undefined} />
    </div>
  );
};

export default AddCareEventPage;