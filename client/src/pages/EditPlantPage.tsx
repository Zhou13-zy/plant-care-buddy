import React, { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import PlantForm from '../components/plants/PlantForm';
import { getPlantById, updatePlant } from '../api/plantService';
import { Plant } from '../models/Plant/plant';
import { UpdatePlantDto } from '../models/Plant/updatePlantDto';
import { formatDate } from '../utils/dateUtils';

const EditPlantPage: React.FC = () => {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const [initialData, setInitialData] = useState<Plant | null>(null);
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    const fetchPlant = async () => {
      if (!id) return;
      try {
        const plant = await getPlantById(id);
        setInitialData(plant);
      } catch (error) {
        console.error('Error fetching plant:', error);
        alert('Failed to load plant data');
      } finally {
        setIsLoading(false);
      }
    };

    fetchPlant();
  }, [id]);

  const handleUpdatePlant = async (data: UpdatePlantDto) => {
    if (!id) return;
    try {
      await updatePlant(Number(id), data);
      alert('Plant updated successfully!');
      navigate(`/plants/${id}`);
    } catch (error) {
      console.error('Error updating plant:', error);
      alert('Failed to update plant');
    }
  };

  if (isLoading) return <div>Loading...</div>;
  if (!initialData) return <div>Plant not found</div>;

  return (
    <div>
      <h1>Edit Plant</h1>
      <PlantForm<UpdatePlantDto>
        onSubmit={handleUpdatePlant} 
        initialData={{
          name: initialData.name,
          species: initialData.species,
          acquisitionDate: formatDate(initialData.acquisitionDate),
          location: initialData.location,
          healthStatus: initialData.healthStatus,
          notes: initialData.notes || '',
          primaryImagePath: initialData.primaryImagePath || ''
        }}
        isEdit={true}
      />
    </div>
  );
};

export default EditPlantPage;