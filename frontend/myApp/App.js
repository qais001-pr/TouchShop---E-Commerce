import React from 'react'
import AppNavigation from './src/Navigation/AppNavigation'
import ProductDetails from './src/screens/ProductDetails'
import { createNativeStackNavigator } from '@react-navigation/native-stack'
import { NavigationContainer } from '@react-navigation/native';
const Stack = createNativeStackNavigator();
export default function App() {

  return (
    <>
      <NavigationContainer>
        <Stack.Navigator initialRouteName="homeScreen" screenOptions={{ headerShown: false }}>
          <Stack.Screen name='homeScreen' component={AppNavigation} />
          <Stack.Screen name="ProductDetails" component={ProductDetails} />
        </Stack.Navigator>
      </NavigationContainer>
    </>
  )
}