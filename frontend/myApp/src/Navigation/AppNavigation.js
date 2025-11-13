import { View, Text, StyleSheet } from 'react-native';
import React from 'react';
import { NavigationContainer } from '@react-navigation/native';
import { createBottomTabNavigator } from '@react-navigation/bottom-tabs';
import ProductsHomePage from '../screens/ProductsHomePage';
import CartScreen from '../screens/CartScreen';
import ProfileScreen from '../screens/ProfileScreen';
import WishlistScreen from '../screens/WishlistScreen';
import MaterialIcons from 'react-native-vector-icons/MaterialIcons';

const Tab = createBottomTabNavigator();

const styles = StyleSheet.create({
    container: {
        flex: 1,
    },
});

export default function AppNavigation() {
    return (
        <View style={styles.container}>
            {/* <NavigationContainer> */}
            <Tab.Navigator
                initialRouteName="Products"
                screenOptions={({ route }) => ({
                    headerShown: false,
                    tabBarActiveTintColor: '#9f97faff',
                    tabBarInactiveTintColor: '#dddddd',
                    tabBarStyle: {
                        height: 90,
                        marginBottom: 5,
                        paddingTop: 20,
                        borderTopLeftRadius: 15,
                        borderTopRightRadius: 15,
                        backgroundColor: '#0d0b28ff',
                    },
                    tabBarIcon: ({ focused, color }) => {
                        let iconName;

                        if (route.name === 'Products') {
                            iconName = focused ? 'home' : 'home';
                        } else if (route.name === 'Cart') {
                            iconName = focused ? 'shopping-cart' : 'shopping-cart';
                        } else if (route.name === 'Wishlist') {
                            iconName = focused ? 'favorite' : 'favorite-border';
                        } else if (route.name === 'Profile') {
                            iconName = focused ? 'person' : 'person-outline';
                        }

                        return <MaterialIcons name={iconName} size={focused ? 32 : 30} color={color} />;
                    },
                })}
            >
                <Tab.Screen
                    name="Products"
                    component={ProductsHomePage}
                    options={({ focused }) => ({
                        title: '',
                        tabBarLabel: '',

                    })}
                />
                <Tab.Screen
                    name="Wishlist"
                    component={WishlistScreen}
                    options={({ focused }) => ({
                        title: '',
                        tabBarLabel: '',
                    })}
                />
                <Tab.Screen
                    name="Cart"
                    component={CartScreen}
                    options={({ focused }) => ({
                        title: '',
                        tabBarLabel: '',

                    })}
                />
                <Tab.Screen
                    name="Profile"
                    component={ProfileScreen}
                    options={({ focused }) => ({
                        title: '',
                        tabBarLabel: '',

                    })}
                />
            </Tab.Navigator>
            {/* </NavigationContainer> */}
        </View>
    );
}