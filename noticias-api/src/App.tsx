// src/App.tsx
import React from 'react';
import NewsList from './components/NewsList';
import './App.css';

const App: React.FC = () => {
  return (
    <div className="App">
      {/* <h1>Últimas Notícias</h1> */}
      <NewsList />
    </div>
  );
};

export default App;
