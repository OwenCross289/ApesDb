import { createLucideIcon } from "lucide-react";

// Adapted from https://svgl.app/library/twitch.svg
export const TwitchIcon = createLucideIcon("twitch", [
  [
    "path",
    {
      d: "M500 0 0 500v1800h600v500l500-500h400l900-900V0H500zm1700 1300-400 400h-400l-350 350v-350H600V200h1600v1100z",
      fill: "currentColor",
      key: "twitch-frame",
      stroke: "none",
      transform: "matrix(0.00857143 0 0 0.00857143 1.714286 0)",
    },
  ],
  [
    "path",
    {
      d: "M1700 550h200v600h-200zm-550 0h200v600h-200z",
      fill: "currentColor",
      key: "twitch-glyphs",
      stroke: "none",
      transform: "matrix(0.00857143 0 0 0.00857143 1.714286 0)",
    },
  ],
]);
