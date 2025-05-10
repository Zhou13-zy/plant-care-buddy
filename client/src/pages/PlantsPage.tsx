import React, { useEffect, useState } from 'react';
import { getAllPlants } from '../api/plantService';
import { Plant } from '../models/plant';
import PlantList from '../components/plants/PlantList';

const PlantsPage: React.FC = () => {
  const [plants, setPlants] = useState<Plant[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    getAllPlants()
      .then(setPlants)
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