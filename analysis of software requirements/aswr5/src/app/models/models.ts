export interface IdentificationData {
  id: string;
  name: string;
  value: boolean;
}

export interface AnalysisData {
  id: string;
  name: string;
  per: number[];
  er: number;
  lrer: number;
  vrer: number;
}

export interface PlanningData {
  id: string;
  name: string;
  value: string;
}

export interface MonitoringData {
  id: string;
  name: string;
  erper: number[];
  erer: number;
  elrer: number;
  evrer: number;
}
