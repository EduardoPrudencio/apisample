// src/types.ts
export interface News {
  id: string;
  author: string;
  title: string;
  content: string;
  link: string;
  image: string;
  publishDate: string;
}

export interface ApiResponse {
  totalFeeds: number;
  totalPages: number;
  feedsPerPages: number;
  currentCount: number;
  page: number;
  feeds: News[];
}
