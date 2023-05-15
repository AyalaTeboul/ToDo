import axios from 'axios';
axios.defaults.baseURL = process.env.REACT_APP_CONNECT_TO_SERVER;


export default {
  getTasks: async () => {
    const result = await axios.get(`/item`)
    return result.data;
  },

  addTask: async (item) => {
    console.log('addTask', item)
    //TODO
    const result = await axios.post(`/item/${item}`)
    return result.data;
  },

  setCompleted: async (id, isComplete) => {
    console.log('setCompleted', { id }, { isComplete })
    //TODO
    const result = await axios.put(`/item/${id}/${isComplete}`)
    return result.data;
  },

  deleteTask: async (id) => {
    console.log('deleteTask')
    await axios.delete(`/item/${id}`)
  }
};
