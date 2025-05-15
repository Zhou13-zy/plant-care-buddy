import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';

import HomePage from './pages/HomePage';
import PlantsPage from './pages/PlantsPage';
import AddPlantPage from './pages/AddPlantPage';
import PlantDetailPage from './pages/PlantDetailPage';
import EditPlantPage from './pages/EditPlantPage';
import EditCareEventPage from './pages/EditCareEventPage';
import AddCareEventPage from './pages/AddCareEventPage';
import AddHealthObservationPage from './pages/AddHealthObservationPage';
import EditHealthObservationPage from './pages/EditHealthObservationPage';

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<HomePage />} />
        <Route path="/plants" element={<PlantsPage />} />
        <Route path="/add-plant" element={<AddPlantPage />} />
        <Route path="/plants/:id" element={<PlantDetailPage />} />
        <Route path="/plants/:id/edit" element={<EditPlantPage />} />
        <Route path="/plants/:plantId/care-events/add" element={<AddCareEventPage />} />
        <Route path="/care-events/:id/edit" element={<EditCareEventPage />} />
        <Route path="/plants/:plantId/health-observations/new" element={<AddHealthObservationPage />} />
        <Route path="/health-observations/:observationId/edit" element={<EditHealthObservationPage />} />
      </Routes>
    </Router>
  );
}

export default App;