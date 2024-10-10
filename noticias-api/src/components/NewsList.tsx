// src/components/NewsList.tsx
import React, { useEffect, useState } from 'react';
import axios from 'axios';
import NewsItem from './NewsItem';
import { News, ApiResponse } from '../types';

const NewsList: React.FC = () => {
  const [newsList, setNewsList] = useState<News[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);
  const [totalPages, setTotalPages] = useState<number>(1);
  const [currentPage, setCurrentPage] = useState<number>(1);

  const fetchNews = async (page: number) => {
    try {
      setLoading(true);
      setError(null);
      const response = await axios.get<ApiResponse>(`http://127.0.0.1:5000/api/news/pagenumber/${page}`);
      
      setNewsList(response.data.feeds);
      setTotalPages(response.data.totalPages);
      setCurrentPage(page);
      setLoading(false);
    } catch (err: any) {
      setError(err.message || 'Erro ao buscar as notícias');
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchNews(1);
  }, []);

  const renderPagination = () => {
    const pageNumbers = [];
    for (let i = 1; i <= totalPages; i++) {
      pageNumbers.push(
        <button
          key={i}
          onClick={() => fetchNews(i)}
          className={`pagination-button ${i === currentPage ? 'active' : ''}`}
        >
          {i}
        </button>
      );
    }
    return pageNumbers;
  };

  if (loading) {
    return <p>Carregando notícias...</p>;
  }

  if (error) {
    return <p>Erro: {error}</p>;
  }

  return (
    <div className="news-list-container">
      <div className="news-list">
        {newsList.map((newsItem) => (
          <NewsItem key={newsItem.id} news={newsItem} />
        ))}
      </div>
      <div className="pagination">
        {renderPagination()}
      </div>
    </div>
  );
};

export default NewsList;
