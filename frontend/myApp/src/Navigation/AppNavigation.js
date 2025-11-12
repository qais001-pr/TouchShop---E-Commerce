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
                            height: 80,
                            marginBottom: 5,
                            paddingTop: 10,
                            borderTopLeftRadius: 15,
                            borderTopRightRadius: 15,
                            backgroundColor: '#0d0b28ff',
                            paddingBottom: 5,
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

                            return <MaterialIcons name={iconName} size={focused ? 30 : 27} color={color} />;
                        },
                    })}
                >
                    <Tab.Screen
                        name="Products"
                        component={ProductsHomePage}
                        options={({ focused }) => ({
                            title: 'Shop',
                            tabBarLabel: 'Shop',

                        })}
                    />
                    <Tab.Screen
                        name="Wishlist"
                        component={WishlistScreen}
                        options={({ focused }) => ({
                            title: 'Wishlist',
                            tabBarLabel: 'Wishlist',
                        })}
                    />
                    <Tab.Screen
                        name="Cart"
                        component={CartScreen}
                        options={({ focused }) => ({
                            title: 'Cart',
                            tabBarLabel: 'Cart',

                        })}
                    />
                    <Tab.Screen
                        name="Profile"
                        component={ProfileScreen}
                        options={({ focused }) => ({
                            title: 'Profile',
                            tabBarLabel: 'Profile',

                        })}
                    />
                </Tab.Navigator>
            {/* </NavigationContainer> */}
        </View>
    );
}