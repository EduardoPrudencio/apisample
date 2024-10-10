// src/components/NewsItem.tsx
import React from 'react';
import { News } from '../types';

interface NewsItemProps {
  news: News;
}

const NewsItem: React.FC<NewsItemProps> = ({ news }) => {
  return (
    <div className="news-item">
      <h2>{news.title}</h2>
      <p><strong>Autor:</strong> {news.author || "Desconhecido"}</p>
      <p><strong>Data de Publicação:</strong> {new Date(news.publishDate).toLocaleString()}</p>
      <a href={news.link} target="_blank" rel="noopener noreferrer">
        <img src={news.image} alt={news.title} style={{ maxWidth: '300px' }} />
      </a>
      <div dangerouslySetInnerHTML={{ __html: news.content }}></div>
    </div>
  );
};

export default NewsItem;
