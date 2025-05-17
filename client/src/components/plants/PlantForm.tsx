import React, { useState } from 'react';
import { CreatePlantDto } from '../../models/Plant/createPlantDto';
import { UpdatePlantDto } from '../../models/Plant/updatePlantDto';
import './PlantForm.css';
import { PlantHealthStatus } from '../../models/Plant/plantHealthStatus';

interface PlantFormProps<T extends CreatePlantDto | UpdatePlantDto> {
  onSubmit: (data: T) => void;
  initialData?: T;
  isEdit?: boolean;
}

const PlantForm = <T extends CreatePlantDto | UpdatePlantDto>({ 
  onSubmit, 
  initialData, 
  isEdit 
}: PlantFormProps<T>) => {
  const [form, setForm] = useState<T>(
    initialData || {
      name: '',
      species: '',
      acquisitionDate: '',
      location: '',
      ...(isEdit ? {} : { healthStatus: PlantHealthStatus.Healthy }), // Only include healthStatus for new plants
      notes: '',
      nextHealthCheckDate: '',
      ...(isEdit ? { id: 0 } : {})
    } as T
  );

  console.log("initialData", initialData);

  const handleChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement | HTMLTextAreaElement>
  ) => {
    const { name, value } = e.target;
    setForm((prev) => ({
      ...prev,
      [name]: name === 'healthStatus' ? Number(value) as PlantHealthStatus : value,
    }));
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    onSubmit(form);
  };

  const healthStatusOptions = Object.entries(PlantHealthStatus)
    .filter(([key, value]) => typeof value === 'number')
    .map(([key, value]) => (
      <option key={value as number} value={value as number}>
        {key}
      </option>
    ));

  return (
    <form onSubmit={handleSubmit} className="plant-form">
      <div className="form-group">
        <label htmlFor="name">Name:</label>
        <input
          type="text"
          id="name"
          name="name"
          value={form.name}
          onChange={handleChange}
          required
        />
      </div>

      <div className="form-group">
        <label htmlFor="species">Species:</label>
        <input
          type="text"
          id="species"
          name="species"
          value={form.species}
          onChange={handleChange}
          required
        />
      </div>

      <div className="form-group">
        <label htmlFor="acquisitionDate">Acquisition Date:</label>
        <input
          type="date"
          id="acquisitionDate"
          name="acquisitionDate"
          value={form.acquisitionDate}
          onChange={handleChange}
          required
        />
      </div>

      <div className="form-group">
        <label htmlFor="location">Location:</label>
        <input
          type="text"
          id="location"
          name="location"
          value={form.location}
          onChange={handleChange}
        />
      </div>

      {!isEdit && (
        <div className="form-group">
          <label htmlFor="healthStatus">Health Status:</label>
          <select
            id="healthStatus"
            name="healthStatus"
            value={(form as any).healthStatus}
            onChange={handleChange}
            required
          >
            {healthStatusOptions}
          </select>
        </div>
      )}

      <div className="form-group">
        <label htmlFor="notes">Notes:</label>
        <textarea
          id="notes"
          name="notes"
          value={form.notes}
          onChange={handleChange}
        />
      </div>

      <div className="form-group">
        <label htmlFor="photo">Photo:</label>
        <input
          type="file"
          id="photo"
          name="photo"
          accept="image/*"
          onChange={(e) => {
            const file = e.target.files?.[0] || null;
            setForm((prev: any) => ({
              ...prev,
              photo: file,
            }));
          }}
        />
      </div>

      <div className="form-group">
        <label htmlFor="nextHealthCheckDate">Next Health Check Date</label>
        <input
          type="date"
          id="nextHealthCheckDate"
          className="form-control"
          value={form.nextHealthCheckDate ? new Date(form.nextHealthCheckDate).toISOString().split('T')[0] : ''}
          onChange={(e) => setForm({
            ...form,
            nextHealthCheckDate: e.target.value ? new Date(e.target.value).toISOString() : null
          })}
        />
      </div>

      <button type="submit" className="submit-button">
        {initialData ? 'Update Plant' : 'Add Plant'}
      </button>
    </form>
  );
};

export default PlantForm;