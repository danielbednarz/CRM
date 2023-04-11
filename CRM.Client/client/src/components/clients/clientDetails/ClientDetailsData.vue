<template>
  <div class="q-ma-md">
    <div class="row" v-if="client">
      <div class="col-md-8 col-xs-12">
        <div class="text-h4">
          {{ client.name }}
          <q-icon
            v-if="client.isActive"
            name="fa-solid fa-square-check"
            color="green"
          >
            <q-tooltip> Klient jest aktywny </q-tooltip>
          </q-icon>
          <q-icon
            v-if="!client.isActive"
            name="fa-solid fa-square-xmark"
            size="md"
            color="red"
          >
            <q-tooltip> Klient nie jest aktywny </q-tooltip>
          </q-icon>
        </div>
        <div class="text-h6 q-my-sm">NIP: {{ client.nip }}</div>
      </div>
      <div class="col-md-4">
        <div class="flex justify-end q-gutter-md q-pb-md">
          <q-btn
            outline
            class="q-py-sm"
            color="secondary"
            label="Edytuj klienta"
            icon-right="fa-solid fa-pen"
            @click="moveToClientEdit"
          ></q-btn>
          <q-btn
            v-if="currentUser.roles.includes($ADMIN)"
            outline
            class="q-py-sm"
            color="negative"
            label="Usuń klienta"
            icon-right="fa-solid fa-trash"
            @click="deleteClientDialogVisible = true"
          ></q-btn>
        </div>
      </div>
    </div>
    <q-separator />
    <div class="row q-mt-md">
      <div class="col-md-8 col-xs-12">
        <div class="row">
          <div class="col-md-5 col-xs-12">Kraj:</div>
          <div class="col-md-7 col-xs-12">{{ client.country }}</div>
        </div>
        <div class="row q-my-sm">
          <div class="col-md-5 col-xs-12">Miejscowość:</div>
          <div class="col-md-7 col-xs-12">{{ client.city }}</div>
        </div>
        <div class="row q-my-sm">
          <div class="col-md-5 col-xs-12">Ulica i numer budynku:</div>
          <div class="col-md-7 col-xs-12">
            {{ client.street }} {{ client.buildingNumber }}
          </div>
        </div>
      </div>
      <div class="col-md-4 col-xs-12">
        <div class="row q-my-sm">
          <div class="col-md-6 col-xs-12">Ocena klienta:</div>
          <div class="col-md-6 col-xs-12">
            <q-rating
              v-model="props.client.rating"
              max="5"
              size="1.25em"
              color="yellow"
              icon="star_border"
              icon-selected="star"
              no-dimming
              readonly
            >
            </q-rating>
            <q-tooltip> {{ props.client.rating }}</q-tooltip>
          </div>
        </div>
        <div class="row q-my-sm">
          <div class="col-md-6 col-xs-12">Krs:</div>
          <div class="col-md-6 col-xs-12">{{ client.krs }}</div>
        </div>
        <div class="row q-my-sm">
          <div class="col-md-6 col-xs-12">REGON:</div>
          <div class="col-md-6 col-xs-12">{{ client.regon }}</div>
        </div>
      </div>
    </div>
    <q-separator />
    <div class="row q-mt-md">
      <div class="col-md-12 q-mb-md">
        <div class="text-h5">Dane kontaktowe</div>
      </div>
      <div class="col-md-6 col-xs-12">
        <div class="text-h6">
          Telefon<q-btn
            flat
            class="q-ml-xs q-mb-xs"
            size="sm"
            color="secondary"
            icon="fa-solid fa-plus"
            @click="addPhoneNumberDialogVisible = true"
          ></q-btn>
        </div>

        <q-separator class="q-mr-md q-mb-sm" />
        <p v-if="client.clientPhoneNumbers.length === 0">Brak</p>
        <p v-for="(item, index) in client.clientPhoneNumbers" :key="index">
          {{ item.phoneNumber }}
          <q-btn
            flat
            class="q-ml-xs q-pb-xs"
            size="sm"
            color="negative"
            icon="fa-solid fa-minus"
            @click="setPhoneNumberToDeleteDialogData(item.id, item.phoneNumber)"
            ><q-tooltip> Usuń numer </q-tooltip>
          </q-btn>
        </p>
      </div>
      <div class="col-md-6 col-xs-12">
        <div class="text-h6">
          Email<q-btn
            flat
            class="q-ml-xs q-mb-xs"
            size="sm"
            color="secondary"
            icon="fa-solid fa-plus"
            @click="addEmailDialogVisible = true"
          ></q-btn>
        </div>
        <q-separator class="q-mb-sm" />
        <p v-if="client.clientEmails.length === 0">Brak</p>
        <p v-for="(item, index) in client.clientEmails" :key="index">
          {{ item.email }}
          <q-btn
            flat
            class="q-ml-xs q-pb-xs"
            size="sm"
            color="negative"
            icon="fa-solid fa-minus"
            @click="setEmailToDeleteDialogData(item.id, item.email)"
            ><q-tooltip> Usuń email </q-tooltip>
          </q-btn>
        </p>
      </div>
    </div>
  </div>
  <q-dialog v-model="deleteClientDialogVisible" persistent>
    <q-card>
      <q-card-section class="row items-center">
        <q-icon
          name="fa-solid fa-triangle-exclamation"
          color="primary"
          size="lg"
        />
        <span class="q-ml-sm" style="font-size: 18px"
          >Czy na pewno chcesz usunąć klienta?</span
        >
      </q-card-section>

      <q-card-actions align="right">
        <q-btn flat label="Anuluj" color="primary" v-close-popup />
        <q-btn
          flat
          label="Tak"
          color="negative"
          @click="deleteClient()"
          v-close-popup
        />
      </q-card-actions>
    </q-card>
  </q-dialog>

  <q-dialog v-model="addPhoneNumberDialogVisible" persistent>
    <q-card style="min-width: 350px">
      <q-card-section>
        <div class="text-h6">Dodaj numer telefonu</div>
      </q-card-section>

      <q-card-section class="q-pt-none">
        <q-input
          dense
          v-model="phoneNumberToAdd"
          autofocus
          @keyup.enter="addPhoneNumberDialogVisible = false"
        />
      </q-card-section>

      <q-card-actions align="right" class="text-primary">
        <q-btn flat label="Anuluj" v-close-popup />
        <q-btn
          flat
          label="Dodaj"
          @click="addClientPhoneNumber()"
          v-close-popup
        />
      </q-card-actions>
    </q-card>
  </q-dialog>

  <q-dialog v-model="deletePhoneNumberDialogVisible" persistent>
    <q-card>
      <q-card-section class="row items-center">
        <q-icon
          name="fa-solid fa-triangle-exclamation"
          color="primary"
          size="lg"
        />
        <span class="q-ml-sm" style="font-size: 18px">
          Czy na pewno chcesz usunąć numer {{ phoneNumberToDelete.phone }}?
        </span>
      </q-card-section>

      <q-card-actions align="right">
        <q-btn flat label="Anuluj" color="primary" v-close-popup />
        <q-btn
          flat
          label="Tak"
          color="negative"
          @click="deleteClientPhoneNumber(phoneNumberToDelete.id)"
          v-close-popup
        />
      </q-card-actions>
    </q-card>
  </q-dialog>

  <q-dialog v-model="addEmailDialogVisible" persistent>
    <q-card style="min-width: 350px">
      <q-card-section>
        <div class="text-h6">Dodaj adres email</div>
      </q-card-section>
      <q-card-section class="q-pt-none">
        <q-input
          dense
          v-model="emailToAdd"
          autofocus
          type="email"
          :rules="emailRules"
        />
      </q-card-section>

      <q-card-actions align="right" class="text-primary">
        <q-btn flat label="Anuluj" v-close-popup />
        <q-btn flat label="Dodaj" @click="addClientEmail()" />
      </q-card-actions>
    </q-card>
  </q-dialog>

  <q-dialog v-model="deleteEmailDialogVisible" persistent>
    <q-card>
      <q-card-section class="row items-center">
        <q-icon
          name="fa-solid fa-triangle-exclamation"
          color="primary"
          size="lg"
        />
        <span class="q-ml-sm" style="font-size: 18px">
          Czy na pewno chcesz usunąć email {{ emailToDelete.email }}?
        </span>
      </q-card-section>

      <q-card-actions align="right">
        <q-btn flat label="Anuluj" color="primary" v-close-popup />
        <q-btn
          flat
          label="Tak"
          color="negative"
          @click="deleteClientEmail(emailToDelete.id)"
          v-close-popup
        />
      </q-card-actions>
    </q-card>
  </q-dialog>
</template>
<script>
import { ref } from "vue";
import { useRoute, useRouter } from "vue-router";
import { useClientsStore } from "../../../stores/clients";
import { useAuthenticationStore } from "../../../stores/authentication";
import { useQuasar } from "quasar";

export default {
  props: {
    client: {
      type: Object,
    },
  },
  setup(props) {
    const route = useRoute();
    const router = useRouter();
    const clientsStore = useClientsStore();
    const $q = useQuasar();
    const currentUser = useAuthenticationStore().currentUser;

    function checkIfEmail(str) {
      const regexExp = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$/gi;

      return regexExp.test(str);
    }

    return {
      props,
      route,
      router,
      currentUser,
      deleteClientDialogVisible: ref(false),
      addPhoneNumberDialogVisible: ref(false),
      addEmailDialogVisible: ref(false),
      showPhoneNumberDeleteButton: ref(false),
      deletePhoneNumberDialogVisible: ref(false),
      deleteEmailDialogVisible: ref(false),
      phoneNumberToAdd: ref(""),
      phoneNumberToDelete: ref({
        id: "",
        phone: "",
      }),
      emailToAdd: ref(""),
      emailToDelete: ref({
        id: "",
        email: "",
      }),
      emailRules: [
        val => (val !== null && val !== '') || 'Pole nie może być puste',
        val => checkIfEmail(val) || "Wartość nie jest adresem email"
      ],
      moveToClientEdit() {
        router.push(`/clients/edit/${route.params.id}`);
      },
      setPhoneNumberToDeleteDialogData(numberId, numberToDelete) {
        this.deletePhoneNumberDialogVisible = true;
        this.phoneNumberToDelete.id = numberId;
        this.phoneNumberToDelete.phone = numberToDelete;
      },
      setEmailToDeleteDialogData(emailId, emailToDelete) {
        this.deleteEmailDialogVisible = true;
        this.emailToDelete.id = emailId;
        this.emailToDelete.email = emailToDelete;
      },
      async deleteClient() {
        await clientsStore.deleteClient().then(
          () => {
            $q.notify({
              type: "info",
              message: `Klient usunięty pomyślnie`,
            });
            router.push(`/clients`);
          },
          (error) => {
            $q.notify({
              type: "negative",
              message: `Błąd przy próbie usunięcia`,
            });
          }
        );
      },
      async addClientPhoneNumber() {
        await clientsStore
          .addClientPhoneNumber(route.params.id, this.phoneNumberToAdd)
          .then(
            () => {
              this.phoneNumberToAdd = "";
              this.addPhoneNumberDialogVisible = false;
              clientsStore.getClientById(route.params.id);
              $q.notify({
                type: "info",
                message: `Numer telefonu dodany pomyślnie`,
              });
            },
            (error) => {
              $q.notify({
                type: "negative",
                message: `Błąd przy próbie dodania numeru telefonu`,
              });
            }
          );
      },
      async deleteClientPhoneNumber(phoneNumberId) {
        await clientsStore.deleteClientPhoneNumber(phoneNumberId).then(() => {
          clientsStore.getClientById(route.params.id);
          $q.notify(
            {
              type: "info",
              message: `Numer telefonu usunięty pomyślnie`,
            },
            (error) => {
              $q.notify({
                type: "negative",
                message: `Błąd przy próbie usunięcia numeru telefonu`,
              });
            }
          );
        });
      },
      async addClientEmail() {
        let res = await clientsStore.addClientEmail(
          route.params.id,
          this.emailToAdd
        );
        if (res.status == 200) {
          $q.notify({
            type: "info",
            message: `Email dodany pomyślnie`,
          });
          this.emailToAdd = "";
          this.addEmailDialogVisible = false;
          clientsStore.getClientById(route.params.id);
        } else {
          $q.notify({
            type: "negative",
            message: `Email nie został dodany`,
          });
        }
      },
      async deleteClientEmail(emailId) {
        let res = await clientsStore.deleteClientEmail(emailId);
        if (res.status == 200) {
          $q.notify({
            type: "info",
            message: `Email usunięty pomyślnie`,
          });
          clientsStore.getClientById(route.params.id);
        } else {
          $q.notify({
            type: "negative",
            message: `Błąd przy próbie usunięcia maila`,
          });
        }
      },
    };
  },
};
</script>
<style scoped>
p {
  color: rgb(114, 114, 114);
}
</style>
