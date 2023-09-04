<template>
  <div class="user-container">
    <table class="table table-striped table-hover table-bordered" id="user-table">
      <thead class="table-dark">
        <tr>
          <th scope="col">Id</th>
          <th scope="col">Username</th>
          <th scope="col">Email</th>
          <th scope="col">Password</th>
          <th scope="col">Role Id</th>
          <th scope="col">Actions</th>
        </tr>
      </thead>
      <tbody>
        <tr>
          <th scope="row">-</th>
          <td><input-component @input="CreateData('username', $event)" /></td>
          <td><input-component @input="CreateData('email', $event)" /></td>
          <td><input-component @input="CreateData('password', $event)" /></td>
          <td><input-component type="number" @input="CreateData('roleId', $event)" /></td>
          <td class="action-create-btn">
            <button class="btn btn-add btn-admin" v-on:click="createUser">
              Create
              <img src="../../assets/Admin/add-user.png" alt="create" />
            </button>
          </td>
        </tr>
        <tr v-for="user in users">
          <th scope="row">{{ user.id }}</th>
          <td><input-component :value='user.username' @input="updateData(user.id, 'username', $event)" /></td>
          <td><input-component :value="user.email" disable="disable" /></td>
          <td><input-component value="**********" type="password" disable="disable" /></td>
          <td><input-component :value="user.role.id" disable="disable" />
          </td>
          <td class="action-btns">
            <button class="btn btn-primary btn-admin" v-on:click="sendUpdateData(user.id, user.username)">
              Update
              <img src="../../assets/Admin/bouton-modifier.png" alt="modify" />
            </button>
            <button class="btn btn-secondary btn-delete btn-admin" v-on:click="deleteUser(user.id)">
              Delete
              <img src="../../assets/Admin/bouton-supprimer.png" alt="delete" />
            </button>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
import { GET, PUT, POST, DELETE } from "@/api/axios";
import { ENDPOINTS } from "@/api/endpoints";
import InputComponent from "@/components/global/InputComponent";
import { createToast } from "mosha-vue-toastify";

export default {
  name: "UsersTab",
  components: {
    InputComponent,
  },
  data() {
    return {
      formData: {
        username: '',
      },
      users: null,
    };
  },
  methods: {
    updateCurrentPage(newPage) {
      this.currentPage = newPage;
    },
    CreateData(key, value) {
      Object.assign(this.formData, { [key]: value });
    },
    updateData(index, key, value) {
      this.users[index - 1][key] = value;
    },
    createUser() {
      POST(ENDPOINTS.CREATE_USER, this.formData, JSON.parse(localStorage.getItem('user')).token)
        .then(() => {
          GET(ENDPOINTS.GET_ALL_USER, JSON.parse(localStorage.getItem('user')).token)
            .then((response) => {
              this.users = response.data
              createToast({ title: 'Created Succesfuly', description: `You created successfuly the ${this.formData.username.toLocaleUpperCase()} user` }, { type: 'success', position: 'bottom-right' });
            })
            .catch((error) => {
              console.dir(error)
            });
        })
        .catch(() => {
          createToast(`An error occured... Please try again`, { type: 'danger', position: 'bottom-right' });
        });
    },
    sendUpdateData(id, username) {
      PUT(ENDPOINTS.UPDATE_USER_ID + `/${id}`, { username: username }, JSON.parse(localStorage.getItem('user')).token)
        .then(() => {
          createToast({ title: 'Updated Succesfuly', description: `You updated successfuly the ${id} user` }, { type: 'success', position: 'bottom-right' });
        })
        .catch(() => {
          createToast(`An error occured... Please try again`, { type: 'danger', position: 'bottom-right' });
        });
    },
    deleteUser(id) {
      DELETE(ENDPOINTS.DELETE_USER + `/${id}`, JSON.parse(localStorage.getItem('user')).token)
        .then(() => {
          GET(ENDPOINTS.GET_ALL_USER, JSON.parse(localStorage.getItem('user')).token)
            .then((response) => {
              this.users = response.data
              createToast({ title: 'Deleted Succesfuly', description: `You deleted successfuly the ${id} user` }, { type: 'success', position: 'bottom-right' });
            })
            .catch((error) => {
              console.dir(error)
            });
        })
        .catch(() => {
          createToast(`An error occured... Please try again`, { type: 'danger', position: 'bottom-right' });
        });
    }
  },
  mounted() {
    GET(ENDPOINTS.GET_ALL_USER, JSON.parse(localStorage.getItem('user')).token)
      .then((response) => {
        this.users = response.data
      })
      .catch((error) => {
        console.dir(error)
      });
  }
};
</script>

<style scoped>
.user-container {
  padding: 1rem;
}

img {
  height: 1rem;
}

.btn-admin {
  border-radius: 1rem;
  box-shadow: 0px 4px 4px 0px rgba(0, 0, 0, 0.25);
  display: flex;
  flex-direction: row;
  gap: 0.5rem;
}

.action-create-btn {
  display: flex;
  justify-content: center;
}

.action-btns {
  display: flex;
  gap: 1rem;
  justify-content: center;
  align-items: center;
}

.btn-add {
  background-color: var(--main-orange);
  color: var(--main-white);
}

.btn-delete {
  background-color: var(--main-red);
  color: var(--main-white);
}

.table {
  --bs-table-border-color: var(--main-black);
}

.table-dark {
  --bs-table-bg: var(--main-green)
}
</style>
