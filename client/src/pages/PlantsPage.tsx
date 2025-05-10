import React, { useEffect, useState } from 'react';
import PlantList, { Plant } from '../components/plants/PlantList';
import api from '../api/axiosConfig';

const PlantsPage: React.FC = () => {
  const [plants, setPlants] = useState<Plant[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    api.get<Plant[]>('/plants')
      .then(response => setPlants(response.data))
      .catch(() => setPlants([]))
      .finally(() => setLoading(false));
  }, []);

  if (loading) return <div>Loading plants...</div>;

  return (
    <div>
      <h1>My Plants</h1>
      <PlantList plants={plants} />
    </div>
  );
};

export default PlantsPage;