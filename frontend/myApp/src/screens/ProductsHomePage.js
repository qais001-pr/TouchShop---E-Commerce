import React, { useCallback, useState } from 'react';
import {
  View,
  Text,
  SafeAreaView,
  StatusBar,
  FlatList,
  ActivityIndicator,
  Modal,
  Image,
  StyleSheet,
  Pressable,
  TextInput,
} from 'react-native';
import { ipAddress } from '../../config/ipAddress';
import { useFocusEffect } from '@react-navigation/native';
import MaterialIcons from 'react-native-vector-icons/MaterialIcons';
const ProductsHomePage = ({ navigation, route }) => {
  const [data, setData] = useState([]);
  const [loading, setLoading] = useState(false);
  const [searchText, setsearchText] = useState('');
  const [showsearchbar, setshowseachbar] = useState(false);

  let fetchAllProducts = async () => {
    try {
      setLoading(true);
      let response = await fetch(`${ipAddress}/api/Product/GetProductDetails`);
      let data, responsedata;
      if (response) {
        data = await response.text();
      }
      if (data) responsedata = JSON.parse(data);
      if (responsedata && responsedata.status === 200) {
        console.log(responsedata)
        setData(responsedata.data);
        setLoading(false);
      } else {
        setData([]);
        setLoading(false);
        ToastAndroid.show('Failed to fetch products', ToastAndroid.SHORT);
      }
    } catch (error) {

    }
  }
  useFocusEffect(
    useCallback(() => {
      fetchAllProducts();
    }, [])
  );
  let getRating = (ratings) => {
    let total = 0;
    if (ratings.length === 0) return "0.0";
    ratings.forEach((r) => {
      total += r.rating;
    });
    let getratingValues = (total / ratings.length).toFixed(1);
    return `${getratingValues}`;
  }


  let filteredData = data.filter((item) =>
    item.product_name.toLowerCase().includes(searchText.toLowerCase())
  );

  let getProductdetailsandNavigate = async (productid) => {
    try {

      setLoading(true);
      let response = await fetch(`${ipAddress}/api/Product/GetProductDetailsByid/${productid}`)
      console.log(response)
      if (response.ok && response.status === 200) {
        let data = await response.text();
        let responsedata = JSON.parse(data);
        navigation.navigate('ProductDetails', { productDetails: responsedata.data });
      }
      setLoading(false);
    } catch (error) {
      setLoading(false);
    }
  }
  const renderProduct = ({ item }) => (

    <Pressable style={styles.card} onPress={() => getProductdetailsandNavigate(item.productid.toString())} >
      {
        item.image_data ? (
          <Image
            source={{ uri: `data:image/png;base64,${item.image_data}` }}
            style={styles.image}
          />
        ) : (
          <View style={[styles.image, styles.imagePlaceholder]}>
            <Text style={{ color: '#aaa' }}>No Image</Text>
          </View>
        )
      }
      <Text style={styles.name}>{item.product_name}</Text>
      <Text style={styles.description} numberOfLines={2}>
        {item.description}
      </Text>
      <View style={{ flexDirection: 'row', justifyContent: 'space-between', alignItems: 'center' }}>
        <Text style={styles.description} numberOfLines={0.3}>{item.category_name}</Text>
        <View style={{ padding: 5, borderRadius: 50, height: 35, width: 35, justifyContent: 'center', alignItems: 'center' }}>
          <Pressable onPress={addToWhishlists} >
            <MaterialIcons name="favorite-border" size={24} color="#b90b0bdd" />
          </Pressable>
        </View>
      </View>
      <View style={{ flexDirection: 'row', justifyContent: 'space-between', alignItems: 'center', marginTop: 5 }}>
        <Text style={styles.price}>Rs. {item.price}</Text>
        <Text>{getRating(item.reviews)} <MaterialIcons name="star" size={16} color="#f1c40f" /></Text>
      </View>
    </Pressable>
  );
  let addToWhishlists = () => {
    console.log("Add to cart clicked");
  }
  return (
    <SafeAreaView style={styles.container}>
      <StatusBar barStyle="dark-content" />
      <View style={styles.header}>
        <Text style={styles.headerText}>TouchShop</Text>
        <Pressable onPress={() => setshowseachbar(!showsearchbar)} >
          <MaterialIcons name="search" size={28} color="#ffffffff" />
        </Pressable>
      </View>
      {
        showsearchbar ?
          <View style={{ margin: 4 }}  >
            <TextInput
              placeholder='Enter Product Name'
              style={{ borderWidth: 2, borderRadius: 40, paddingLeft: 15 }}
              keyboardType='default'
              value={searchText}
              onChangeText={setsearchText} />
          </View>
          : null
      }
      <View>
        <Text style={{ fontSize: 22, fontWeight: 'bold', margin: 15 }}>Products</Text>
      </View>
      {
        data.length > 0 ? (
          <FlatList
            data={filteredData}
            renderItem={renderProduct}
            keyExtractor={(item) => item.productid.toString()}
            numColumns={2}
            columnWrapperStyle={styles.row}
            contentContainerStyle={styles.listContainer}
            showsVerticalScrollIndicator={false}
          />
        ) : (
          !loading && (
            <View style={styles.emptyContainer}>
              <Text style={styles.emptyText}>No Products Available</Text>
            </View>
          )
        )
      }

      <Modal transparent={true} animationType="fade" visible={loading}>
        <View style={styles.loadingContainer}>
          <ActivityIndicator size="large" color="#141468ff" />
        </View>
      </Modal>
    </SafeAreaView >
  );
};

export default ProductsHomePage;

const styles = StyleSheet.create({
  container: {
    flex: 1,
    marginTop: StatusBar.currentHeight || 0,
    backgroundColor: '#f5f5f5',
  },
  header: {
    backgroundColor: '#0c3a77ff',
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    paddingVertical: 15,
    paddingHorizontal: 20,
  },
  headerText: {
    color: '#ffffff',
    fontSize: 20,
    fontWeight: 'bold',
  },
  listContainer: {
    paddingHorizontal: 10,
    paddingBottom: 20,
  },
  row: {
    justifyContent: 'space-evenly',
  },
  card: {
    backgroundColor: '#fff',
    borderRadius: 5,
    borderWidth: 0.6,
    marginVertical: 8,
    flex: 0.48,
    padding: 10,
    shadowColor: '#000',
    shadowOpacity: 0.1,
    shadowRadius: 5,
    elevation: 3,
  },
  image: {
    width: '100%',
    height: 130,
    borderRadius: 8,
    marginBottom: 10,
  },
  imagePlaceholder: {
    justifyContent: 'center',
    alignItems: 'center',
    backgroundColor: '#eaeaea',
  },
  name: {
    fontSize: 16,
    fontWeight: 'bold',
    color: '#222',
  },
  description: {
    fontSize: 13,
    color: '#555',
    marginVertical: 4,
  },
  price: {
    fontSize: 15,
    color: '#0c3a77ff',
    fontWeight: 'bold',
  },
  emptyContainer: {
    alignItems: 'center',
    marginTop: 50,
  },
  emptyText: {
    fontSize: 18,
    fontWeight: 'bold',
  },
  loadingContainer: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
  },
});
