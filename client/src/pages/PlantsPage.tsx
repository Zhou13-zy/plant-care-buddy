import React, { useEffect, useState } from 'react';
import { getAllPlants, searchPlants } from '../api/plantService';
import { Plant } from '../models/plant';
import PlantList from '../components/plants/PlantList';
import SearchBar from '../components/plants/SearchBar';
import { useSearchParams } from 'react-router-dom';

const PlantsPage: React.FC = () => {
  const [plants, setPlants] = useState<Plant[]>([]);
  const [loading, setLoading] = useState(true);
  const [filters, setFilters] = useState({
    name: '',
    species: '',
    healthStatus: '',
    location: ''
  });

  const [searchParams, setSearchParams] = useSearchParams();

  useEffect(() => {
    const name = searchParams.get('name') || '';
    const species = searchParams.get('species') || '';
    const healthStatus = searchParams.get('healthStatus') || '';
    const location = searchParams.get('location') || '';
    setFilters({ name, species, healthStatus, location });

    if (name || species || healthStatus || location) {
      handleSearch({ name, species, healthStatus: healthStatus ? Number(healthStatus) : undefined, location });
    } else {
      getAllPlants()
        .then(setPlants)
        .catch(() => setPlants([]))
        .finally(() => setLoading(false));
    }
  }, []);

  const handleSearch = async (filters: {
    name?: string;
    species?: string;
    healthStatus?: number;
    location?: string;
  }) => {
    setLoading(true);

    const params: any = {};
    if (filters.name) params.name = filters.name;
    if (filters.species) params.species = filters.species;
    if (filters.healthStatus !== undefined) params.healthStatus = filters.healthStatus;
    if (filters.location) params.location = filters.location;
    setSearchParams(params);

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