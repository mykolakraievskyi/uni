export interface DataModel {
  inputData: number[];
  average?: number;
  mode?: number[] | null;
  median?: number;
  range?: number;
  dispersion?: number;
  stdDev?: number;
  correctedDispersion?: number;
  correctedStdDev?: number;
  variation?: number;
  pm2?: number;
  cm2?: number;
  asymmetry?: number;
  excess?: number;
}
