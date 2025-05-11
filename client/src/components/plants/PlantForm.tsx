import React, { useState } from 'react';
import { CreatePlantDto } from '../../models/createPlantDto';
import './PlantForm.css';

interface PlantFormProps {
  onSubmit: (data: CreatePlantDto) => void;
  initialData?: CreatePlantDto;
}

const healthStatusOptions = [
  { value: 0, label: 'Healthy' },
  { value: 1, label: 'NeedsAttention' },
  { value: 2, label: 'Unhealthy' },
  { value: 3, label: 'Dormant' },
];

const PlantForm: React.FC<PlantFormProps> = ({ onSubmit, initialData }) => {
  const [form, setForm] = useState<CreatePlantDto>(
    initialData || {
      name: '',
      species: '',
      acquisitionDate: '',
      location: '',
      healthStatus: 0,
      notes: '',
      primaryImagePath: '',
    }
  );

  const handleChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement | HTMLTextAreaElement>
  ) => {
    const { name, value } = e.target;
    setForm((prev) => ({
      ...prev,
      [name]: name === 'healthStatus' ? Number(value) : value,
    }));
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    onSubmit(form);
  };

  return (
    <form onSubmit={handleSubmit} className="plant-form">
      <label>
        Name:
        <input
          name="name"
          value={form.name}
          onChange={handleChange}
          required
          maxLength={100}
        />
      </label>
      <label>
        Species:
        <input
          name="species"
          value={form.species}
          onChange={handleChange}
          required
          maxLength={100}
        />
      </label>
      <label>
        Acquisition Date:
        <input
          type="date"
          name="acquisitionDate"
          value={form.acquisitionDate}
          onChange={handleChange}
          required
        />
      </label>
      <label>
        Location:
        <input
          name="location"
          value={form.location || ''}
          onChange={handleChange}
          maxLength={200}
        />
      </label>
      <label>
        Health Status:
        <select
          name="healthStatus"
          value={form.healthStatus}
          onChange={handleChange}
          required
        >
          {healthStatusOptions.map((opt) => (
            <option key={opt.value} value={opt.value}>
              {opt.label}
            </option>
          ))}
        </select>
      </label>
      <label>
        Notes:
        <textarea
          name="notes"
          value={form.notes || ''}
          onChange={handleChange}
          maxLength={1000}
        />
      </label>
      <label>
        Primary Image Path:
        <input
          name="primaryImagePath"
          value={form.primaryImagePath || ''}
          onChange={handleChange}
          maxLength={500}
        />
      </label>
      <button type="submit">Save Plant</button>
    </form>
  );
};

export default PlantForm;