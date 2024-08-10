import React, { createContext, FC } from 'react'
import { Configuration } from '../../type'

export const SharedContext = createContext<Configuration>({} as Configuration)
