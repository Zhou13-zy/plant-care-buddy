import React, { useEffect, useState } from 'react';
import { getAllPlants, searchPlants } from '../api/plantService';
import { Plant } from '../models/plant';
import PlantList from '../components/plants/PlantList';
import SearchBar from '../components/plants/SearchBar';

const PlantsPage: React.FC = () => {
  const [plants, setPlants] = useState<Plant[]>([]);
  const [loading, setLoading] = useState(true);
  const [filters, setFilters] = useState({
    name: '',
    species: '',
    healthStatus: '',
    location: ''
  });

  useEffect(() => {
    getAllPlants()
      .then(setPlants)
      .catch(() => setPlants([]))
      .finally(() => setLoading(false));
  }, []);

  const handleSearch = async (filters: {
    name?: string;
    species?: string;
    healthStatus?: number;
    location?: string;
  }) => {
    setLoading(true);
    try {
      const results = await searchPlants(filters);
      setPlants(results);
    } catch {
      setPlants([]);
    } finally {
      setLoading(false);
    }
  };

  if (loading) return <div>Loading plants...</div>;

  return (
    <div>
      <h1>My Plants</h1>
      <SearchBar
        filters={filters}
        setFilters={setFilters}
        onSearch={handleSearch}
      />
      <PlantList plants={plants} />
    </div>
  );
};

export default PlantsPage;