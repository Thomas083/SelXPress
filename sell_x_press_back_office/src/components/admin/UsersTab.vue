<template>
  <div>
    <table class="table table-striped table-hover table-bordered">
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
          <td><input-component /></td>
          <td><input-component /></td>
          <td><input-component /></td>
          <td><input-component type="number" /></td>
          <td>
            <button class="btn btn-add btn-admin">
              Create
              <img src="../../assets/Admin/add-user.png" alt="create" />
            </button>
          </td>
        </tr>
        <tr v-for="user in users">
          <th scope="row">{{ user.id }}</th>
          <td><input-component :value='user.username' @input="updateData(user.id, 'username', $event )" /></td>
          <td><input-component :value="user.email" @input="updateData(user.id, 'email', $event )" /></td>
          <td><input-component :value="user.password" @input="updateData(user.id, 'password', $event )" /></td>
          <td><input-component :value="user.roleId" @input="updateData(user.id, 'roleId', $event )" /></td>
          <td>
            <button class="btn btn-primary btn-admin" v-on:click="sendUpdateData(user.id)">
              Update
              <img src="../../assets/Admin/bouton-modifier.png" alt="modify" />
            </button>
            <button class="btn btn-secondary btn-delete btn-admin">
              Delete
              <img src="../../assets/Admin/bouton-supprimer.png" alt="delete" />
            </button>
          </td>
        </tr>
      </tbody>
      <tfoot>
        <nav aria-label="Page navigation example">
          <ul class="pagination justify-content-end">
            <li class="page-item disabled">
              <a class="page-link" href="#" tabindex="-1" aria-disabled="true">Previous</a>
            </li>
            <li class="page-item"><a class="page-link" href="#">1</a></li>
            <li class="page-item"><a class="page-link" href="#">2</a></li>
            <li class="page-item"><a class="page-link" href="#">3</a></li>
            <li class="page-item">
              <a class="page-link" href="#">Next</a>
            </li>
          </ul>
        </nav>
      </tfoot>
    </table>
  </div>
</template>

<script>
import InputComponent from "@/components/global/InputComponent";
export default {
  name: "UsersTab",
  components: {
    InputComponent,
  },
  data() {
    return {
      users: [
        {
          id: 1,
          username: 'tester',
          email: 'tester@gmail.com',
          password: 'unpassword',
          roleId: '1'
        },
        {
          id: 2,
          username: 'tester2',
          email: 'testermax@gmail.com',
          password: 'unpassword2',
          roleId: '1'
        },
      ],
    };
  },
  methods: {
    updateData(index, key, value) {
      this.users[index-1][key] = value;
    },
    sendUpdateData(index) {
      console.dir(this.users[index-1])
    }
  },
};
</script>

<style scoped>
img {
  height: 1rem;
}

.btn-admin {
  border-radius: 1rem;
  box-shadow: 0px 4px 4px 0px rgba(0, 0, 0, 0.25);
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
