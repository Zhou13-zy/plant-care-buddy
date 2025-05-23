import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import NavBar from "./components/common/NavBar";

import HomePage from './pages/HomePage';
import PlantsPage from './pages/PlantsPage';
import AddPlantPage from './pages/AddPlantPage';
import PlantDetailPage from './pages/PlantDetailPage';
import EditPlantPage from './pages/EditPlantPage';
import EditCareEventPage from './pages/EditCareEventPage';
import AddCareEventPage from './pages/AddCareEventPage';
import EditHealthObservationPage from './pages/EditHealthObservationPage';
import DashboardPage from './pages/DashboardPage';

function App() {
  return (
    <Router>
      <NavBar />
      <Routes>
        <Route path="/" element={<HomePage />} />
        <Route path="/plants" element={<PlantsPage />} />
        <Route path="/add-plant" element={<AddPlantPage />} />
        <Route path="/plants/:id" element={<PlantDetailPage />} />
        <Route path="/plants/:id/edit" element={<EditPlantPage />} />
        <Route path="/plants/:plantId/care-events/add" element={<AddCareEventPage />} />
        <Route path="/care-events/:id/edit" element={<EditCareEventPage />} />
        <Route path="/health-observations/:observationId/edit" element={<EditHealthObservationPage />} />
        <Route path="/dashboard" element={<DashboardPage />} />
      </Routes>
    </Router>
  );
}

export default App;